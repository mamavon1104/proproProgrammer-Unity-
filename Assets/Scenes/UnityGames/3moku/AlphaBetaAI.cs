using moku3;
using UnityEngine;

public class AlphaBetaAI
{
    private Manager3Mokunarabe gameManager;

    public AlphaBetaAI(Manager3Mokunarabe manager)
    {
        gameManager = manager;
    }

    public (int x, int y) GetBestMove()
    {
        int bestScore = int.MinValue;
        (int x, int y) bestMove = (-1, -1);

        var board = gameManager.GetTiles;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == PlayerColor.none)
                {
                    board[i, j] = PlayerColor.black;
                    int score = AlphaBeta(board, 1, false, int.MinValue, int.MaxValue);
                    board[i, j] = PlayerColor.none;

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = (i, j);
                    }
                }
            }
        }

        return bestMove;
    }

    private int AlphaBeta(PlayerColor[,] board, int depth, bool isMaximizing, int alpha, int beta)
    {
        if (gameManager.CheckWin(board))
        {
            return isMaximizing ? -10 + depth : 10 - depth;
        }

        if (IsBoardFull(board))
        {
            return 0;
        }

        gameManager.DebugTiles(board);

        if (isMaximizing)
        {
            int maxEval = int.MinValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == PlayerColor.none)
                    {
                        board[i, j] = PlayerColor.black;
                        int eval = AlphaBeta(board, depth + 1, false, alpha, beta);
                        board[i, j] = PlayerColor.none;
                        maxEval = Mathf.Max(maxEval, eval);
                        alpha = Mathf.Max(alpha, eval);
                        if (beta <= alpha)
                            break;
                    }
                }
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == PlayerColor.none)
                    {
                        board[i, j] = PlayerColor.white;
                        int eval = AlphaBeta(board, depth + 1, true, alpha, beta);
                        board[i, j] = PlayerColor.none;
                        minEval = Mathf.Min(minEval, eval);
                        beta = Mathf.Min(beta, eval);
                        if (beta <= alpha)
                            break;
                    }
                }
            }
            return minEval;
        }
    }

    private bool IsBoardFull(PlayerColor[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == PlayerColor.none)
                    return false;
            }
        }
        return true;
    }
}
