using Mamavon.Funcs;
using UniRx;
using UnityEngine;

public enum BattleState
{
    PlayerTurn,
    EnemyTurn,
}
public enum GameEndState
{
    NotFinished,
    PlayerVictory,
    PlayerDefeat
}
public class TurnBattleManager : MonoBehaviour
{
    public ReactiveProperty<GameEndState> currentGameEndState = new ReactiveProperty<GameEndState>(GameEndState.NotFinished);

    public ReactiveProperty<BattleState> currentButtleState = new ReactiveProperty<BattleState>(BattleState.PlayerTurn);

    [SerializeField] TurnBattlePlayer player;
    [SerializeField] TurnBattleEnemy[] enemies;

    private static TurnBattleManager _instance;
    public static TurnBattleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<TurnBattleManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("TurnBattleManager");
                    _instance = go.AddComponent<TurnBattleManager>();
                }
            }
            return _instance;
        }
    }


    private void Start()
    {
        currentGameEndState.Subscribe(state =>
        {
            switch (state)
            {
                case GameEndState.PlayerVictory:
                    "playerの勝利！".Debuglog(TextColor.Blue);
                    break;
                case GameEndState.PlayerDefeat:
                    "Playerのまけー＾～＾".Debuglog(TextColor.Red);
                    break;
            }
        });

        currentButtleState.Subscribe(state =>
        {
            if (state != BattleState.EnemyTurn)
            {
                "プレイヤーのターンです。どうぞ。".Debuglog(TextColor.Yellow);
                return;
            }

            "敵のターン来たー！！".Debuglog(TextColor.Red);
            foreach (var enemy in enemies)
            {
                enemy.Attack(player);
            }
            CheckBattleState();
        });
    }

    private void CheckBattleState()
    {
        if (player.IsDead())
            currentGameEndState.Value = GameEndState.PlayerDefeat;
        else
        {
            bool allEnemiesDefeated = true;
            foreach (var enemy in enemies)
            {
                if (!enemy.IsDead())
                {
                    allEnemiesDefeated = false;
                    break;
                }
            }

            if (allEnemiesDefeated)
                currentGameEndState.Value = GameEndState.PlayerVictory;
            else
                currentButtleState.Value = BattleState.PlayerTurn;
        }
    }
}