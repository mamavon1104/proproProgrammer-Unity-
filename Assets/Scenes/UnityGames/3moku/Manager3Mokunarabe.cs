using Mamavon.Funcs;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace moku3
{

    public enum PlayerColor
    {
        none,
        white,
        black
    }
    public class Manager3Mokunarabe : MonoBehaviour
    {
        ReactiveProperty<PlayerColor> player = new ReactiveProperty<PlayerColor>();
        private PlayerColor[,] tiles = new PlayerColor[3, 3];
        private GameObject[,] t = new GameObject[3, 3];

        public PlayerColor[,] GetTiles
        {
            get
            {
                return tiles;
            }
        }
        private void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    t[i, j] = transform.GetChild(i).GetChild(j).gameObject;
                    t[i, j].TryGetComponent<Tiles3Moku>(out var tile);

                    tile.SetManager = this;
                    tile.SetValues = (i, j);
                }
            }
            player.Value = PlayerColor.white;

            player.Subscribe(color =>
            {
                // 敵のターンになったら
                if (color == PlayerColor.black)
                {
                    AlphaBetaAI ai = new AlphaBetaAI(this);

                    var pos = ai.GetBestMove();

                    SetPieces(pos);
                }
            });
        }


        public void SetPieces((int x, int y) num)
        {
            if (tiles[num.x, num.y] != PlayerColor.none)
                return;

            tiles[num.x, num.y] = player.Value;
            t[num.x, num.y].GetComponent<Renderer>().material.color = player.Value == PlayerColor.white ? Color.white : Color.black;
            player.Value = player.Value == PlayerColor.white ? PlayerColor.black : PlayerColor.white;

            if (CheckWin(tiles))
                Debug.Log("finish");
        }

        public bool CheckWin(PlayerColor[,] tiles)
        {
            // 横方向のチェック
            for (int i = 0; i < 3; i++)
            {
                if (tiles[i, 0] != PlayerColor.none &&
                    tiles[i, 0] == tiles[i, 1] && tiles[i, 1] == tiles[i, 2])
                {
                    return true;
                }
            }

            // 縦方向のチェック
            for (int j = 0; j < 3; j++)
            {
                if (tiles[0, j] != PlayerColor.none &&
                    tiles[0, j] == tiles[1, j] && tiles[1, j] == tiles[2, j])
                {
                    return true;
                }
            }

            // 対角線のチェック
            if (tiles[0, 0] != PlayerColor.none &&
                tiles[0, 0] == tiles[1, 1] && tiles[1, 1] == tiles[2, 2])
            {
                return true;
            }

            if (tiles[0, 2] != PlayerColor.none &&
                tiles[0, 2] == tiles[1, 1] && tiles[1, 1] == tiles[2, 0])
            {
                return true;
            }

            // 勝利条件を満たさない場合
            return false;
        }

        public void DebugTiles(PlayerColor[,] tiles)
        {
            Dictionary<PlayerColor, string> keyValuePairs = new Dictionary<PlayerColor, string>()
            {
                { PlayerColor.white,"w" },
                { PlayerColor.black,"b" },
                { PlayerColor.none,"=" },
            };

            string s = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    s += keyValuePairs[tiles[i, j]];
                    s += " ";
                }
                s += "\n";
            }
            s.Debuglog();
        }
    }
}