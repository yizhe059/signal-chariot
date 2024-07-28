using InGame.Boards.Signals;
using InGame.UI;
using InGame.Cores;

namespace InGame.InGameStates
{
    public class BoardBattleState: InGameState
    {
        public override InGameStateType type => InGameStateType.BoardBattleState;

        private TimeEffectManager m_timeEffectManager;
        private SignalController m_signalController;
        
        public override void Enter(InGameState last)
        {
            m_timeEffectManager.Reset();
            m_signalController.Reset();
            
            m_timeEffectManager.Start();
            m_signalController.Start();

            BattleProgressUI.Instance.Hide();
            BattleResultUI.Instance.Hide();
            ChariotStatusUI.Instance.Show();
            NavigationBarUI.Instance.Show();
            BoardBarUI.Instance.Show();
        }

        public override void Exit()
        {
            m_timeEffectManager.Stop();
            m_signalController.Stop();

            BattleProgressUI.Instance.Show();
            BattleResultUI.Instance.Show();
            ChariotStatusUI.Instance.Hide();
            NavigationBarUI.Instance.Hide();
            BoardBarUI.Instance.Hide();
        }

        public static BoardBattleState CreateState(TimeEffectManager timeEffectManager, SignalController signalController)
        {
            return new BoardBattleState
            {
                m_timeEffectManager = timeEffectManager,
                m_signalController = signalController
            };

        }
    }
}