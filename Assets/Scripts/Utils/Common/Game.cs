namespace Utils.Common
{
    public class GameState : IState<GameState>
    {
        public virtual void Enter(GameState last)
        {

        }

        public virtual void Exit()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void LateUpdate()
        {

        }
    }

    public class Game : MonoSingleton<Game>
    {
        private StateMachine<GameState> m_stateMachine = new StateMachine<GameState>();

        public GameState currentState => m_stateMachine.current;

        public GameState nextState
        {
            get => m_stateMachine.next;
            set => m_stateMachine.next = value;
        }

        private void Update()
        {
            var state = m_stateMachine.current;
            if (state != null)
            {
                m_stateMachine.isLocked = true;
                state.Update();
                m_stateMachine.isLocked = false;
            }
        }

        private void LateUpdate()
        {
            var state = m_stateMachine.current;
            if (state != null)
            {
                m_stateMachine.isLocked = true;
                state.LateUpdate();
                m_stateMachine.isLocked = false;
            }
        }
    }

}