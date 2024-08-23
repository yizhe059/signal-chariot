using Utils.Common;

namespace InGame.InGameStates
{
    public enum InGameStateType
    {
        None,
        // Board Scene Sub-states
        AddSlotState,
        BoardWaitingState,
        ModulePlacingState,
        BoardTestState,
        // Battle Scene Sub-states
        BattleState,
        BattleResultState,
        RewardState
    }
    public abstract class InGameState : IState<InGameState>
    {
        public abstract InGameStateType type { get; }

        public virtual void Enter(InGameState last){}

        public virtual void Update(){}
        
        public virtual void LateUpdate(){}

        public virtual void Exit(){}
    }
}