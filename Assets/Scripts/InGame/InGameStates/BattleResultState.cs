using UnityEngine;

using InGame.Cores;
using InGame.UI;

namespace InGame.InGameStates
{
    public enum BattleResultType
    {
        WaveWin,
        BattleWin,
        GameWin,
        Fail
    }

    public class BattleResultState : InGameState
    {
        public override InGameStateType type => InGameStateType.BattleResultState;

        private BattleResultType m_resultType;

        public override void Enter(InGameState last)
        {
            var cameraManager = GameManager.Instance.GetCameraManager();
            cameraManager.BoardCameraSetActive(false);
            cameraManager.MiniBoardCameraSetActive(false);
            cameraManager.BattleCameraSetActive(true);
            
            Debug.Log("Enter battle result");

            BattleProgressUI.Instance.Show();

            BattleResultUI.Instance.Show();
            BattleResultUI.Instance.Switch(m_resultType);

            AndroidStatusUI.Instance.Show();
            NavigationBarUI.Instance.Hide();
            BoardBarUI.Instance.Hide();

            // TODO disable battle field interactions
        }
        
        public override void Exit()
        {
            Debug.Log("Exit battle result");
            BattleProgressUI.Instance.Hide();
            BattleResultUI.Instance.Hide();
            AndroidStatusUI.Instance.Hide();
            NavigationBarUI.Instance.Show();
            BoardBarUI.Instance.Show();

            // TODO enable battle field interactions
        }

        public static BattleResultState CreateState(BattleResultType resultType)
        {
            var state = new BattleResultState
            {
                m_resultType = resultType,
            };
            return state;
        }
    }
}