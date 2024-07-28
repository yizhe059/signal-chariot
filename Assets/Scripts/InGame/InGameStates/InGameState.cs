using Utils.Common;

namespace InGame.InGameStates
{
    public enum InGameStateType
    {
        None,
        AddSlotState,
        BoardWaitingState,
        ModulePlacingState,
        BoardBattleState,
        BattleState,
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