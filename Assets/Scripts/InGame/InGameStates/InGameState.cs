using Utils.Common;

namespace InGame.InGameStates
{
    public enum InGameStateType
    {
        None,
        // Preparation Sub-states
        AddSlotState,
        BoardWaitingState,
        ModulePlacingState,
        BoardTestState,
        // Battle Sub-states
        BattleState,
        BattleResultState,
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