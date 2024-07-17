using Utils.Common;

namespace World.InGameStates
{
    public enum InGameStateType
    {
        None
    }
    public abstract class InGameState : IState<InGameState>
    {
        public abstract InGameStateType type { get; }

        public virtual void Enter(InGameState last)
        {
            
        }

        public virtual void Update()
        {
            
        }
        
        public virtual void LateUpdate()
        {
            
        }

        public virtual void Exit()
        {
            
        }


    }
}