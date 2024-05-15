using TMPro;
using UnityEngine;

public class RefactoringLesson01 : MonoBehaviour
{
    public TextMeshProUGUI buttonChangeText;

    void Start()
    {
        buttonChangeText.text = "Please Press Button";
    }
    public void OnPressButton()
    {
        buttonChangeText.text = "Pressed!!";
    }
}
namespace Dummy.StatePattern
{
    using UnityEngine;

    public enum StateType
    {
        UNDEFINED,
        IDLE,
        MOVE,
        ATTACK,
        DEFENSE,
        DEAD,
    }

    public interface IState
    {
        StateType GetCurrentState { get; }
        bool ChangeState(IState nextState);

        void OnStateChanged();
        void OnStateBegin();
        void OnStateEnd();

        void Update(float deltaTime);
        void SetNextState(IState nextState);
        IState GetNextState();
    }

    public class IdleState : IState
    {
        private IState m_nextState = null;
        public bool IsEndState { get; protected set; } = false;
        #region ===== IState =====

        StateType IState.GetCurrentState { get; } = StateType.IDLE;

        bool IState.ChangeState(IState nextState)
        {
            IState state = this;
            state.OnStateEnd();
            if (nextState == null) return false;

            nextState.OnStateChanged();
            nextState.OnStateBegin();
            return true;
        }

        void IState.OnStateChanged()
        {
            // Initialize
        }

        void IState.OnStateBegin()
        {
            IsEndState = false;
        }

        void IState.OnStateEnd()
        {
            IsEndState = true;
        }

        void IState.Update(float deltaTime)
        {
            if (IsEndState) return;
            if (m_nextState != null)
            {
                (this as IState).ChangeState(m_nextState);
                return;
            }
        }

        void IState.SetNextState(IState nextState) { m_nextState = nextState; }
        IState IState.GetNextState() { return m_nextState; }

        #endregion //) ===== IState =====
    }

    public class MoveState : IState
    {
        protected Transform m_moveTarget = null;
        protected Vector3 m_moveDestination = Vector3.zero;
        protected float m_moveSpeed = 0.0f;
        private IState m_nextState = null;
        public bool IsEndState { get; protected set; } = false;
        #region ===== IState =====

        StateType IState.GetCurrentState { get; } = StateType.MOVE;

        bool IState.ChangeState(IState nextState)
        {
            IState state = this;
            state.OnStateEnd();
            if (nextState == null) return false;

            nextState.OnStateChanged();
            nextState.OnStateBegin();
            return true;
        }

        void IState.OnStateChanged()
        {
            // Initialize
        }

        void IState.OnStateBegin()
        {
            IsEndState = false;
        }

        void IState.OnStateEnd()
        {
            IsEndState = true;
        }

        void IState.Update(float deltaTime)
        {
            if (IsEndState) return;
            if (m_nextState != null)
            {
                (this as IState).ChangeState(m_nextState);
                return;
            }

            if (m_moveTarget == null) return;
            Vector3 currentPosition = m_moveTarget.position;
            Vector3 moveVec = (m_moveDestination - currentPosition).normalized;
            m_moveTarget.position = currentPosition + moveVec * (m_moveSpeed * deltaTime);
        }

        void IState.SetNextState(IState nextState) { m_nextState = nextState; }
        IState IState.GetNextState() { return m_nextState; }

        #endregion //) ===== IState =====

        public void SetTarget(Transform target, Vector3 destination, float speedPerSec)
        {
            m_moveTarget = target;
            m_moveDestination = destination;
            m_moveSpeed = speedPerSec;
        }
    }


    public class StateController : MonoBehaviour
    {
        private IState currentState = new IdleState();


        private void Update()
        {
            currentState.Update(Time.deltaTime);
        }
    }
}
