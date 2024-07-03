using UnityEngine;

public interface UIState
{
    public abstract void Enter();
    public abstract void Exit();
}

// �ʏ��UI��Ԃ̃N���X
public class NoneButton : UIState
{
    public void Enter()
    {
        Debug.Log("�����A�������悤�B");
    }

    public void Exit()
    {
        Debug.Log("�s���J�n�I");
    }
}

public class SelectChara : UIState
{
    public void Enter()
    {
        Debug.Log("�L������I�����Ă�������");
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