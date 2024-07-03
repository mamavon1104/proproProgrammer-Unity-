using UnityEngine;

public interface UIState
{
    public abstract void Enter();
    public abstract void Exit();
}

// 通常のUI状態のクラス
public class NoneButton : UIState
{
    public void Enter()
    {
        Debug.Log("さあ、何をしよう。");
    }

    public void Exit()
    {
        Debug.Log("行動開始！");
    }
}

public class SelectChara : UIState
{
    public void Enter()
    {
        Debug.Log("キャラを選択してください");
    }

    public void Exit()
    {
        Debug.Log("");
    }
}

public class ItemUIState : UIState
{
    public void Enter()
    {
        Debug.Log("Button is Pressed");
    }

    public void Exit()
    {

    }
}