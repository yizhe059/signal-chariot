using UnityEngine;

using InGame.Cores;
using InGame.UI;
using InGame.Views;
using InGame.BattleFields.Chariots;

namespace InGame.InGameStates
{
    public class BattleState : InGameState
    {
        public override InGameStateType type => InGameStateType.BattleState;
        private ChariotView m_chariotView;
        private Chariot m_chariot;

        public override void Enter(InGameState last)
        {
            Debug.Log("Enter battle");

            var boardView = GameManager.Instance.GetBoardView();
            boardView.GetActiveBoardCornerPos(out var minPos, out var maxPos);

            var center = (minPos + maxPos) / 2;
            var delta = maxPos - minPos;
            var length = Mathf.Max(delta.x, delta.y);
            
            var cameraManager = GameManager.Instance.GetCameraManager();
            cameraManager.BoardCameraSetActive(false);
            
            cameraManager.MiniBoardCameraSetActive(true);
            cameraManager.SetMiniBoardCameraPosition(center);
            cameraManager.SetMiniBoardCameraSize(length / 2);
            
            cameraManager.BattleCameraSetActive(true);
            
            
            GameManager.Instance.GetInputManager().RegisterMoveEvent(OnMoveKeyPressed);

            BattleProgressUI.Instance.Show();
            BattleResultUI.Instance.Hide();
            ChariotStatusUI.Instance.Show();
            NavigationBarUI.Instance.Hide();
            BoardBarUI.Instance.Hide();

            // TODO minimise board view
        }
        
        public override void Exit()
        {
            Debug.Log("Exit battle");
            GameManager.Instance.GetInputManager().UnregisterMoveEvent(OnMoveKeyPressed);

            BattleProgressUI.Instance.Hide();
            BattleResultUI.Instance.Show();
            ChariotStatusUI.Instance.Hide();
            NavigationBarUI.Instance.Show();
            BoardBarUI.Instance.Show();

            // TODO restore board view
        }
        
        private void OnMoveKeyPressed(Vector2 inputDirection)
        {
            m_chariotView.SetMoveDirection(inputDirection);
        }

        public static BattleState CreateState(Chariot chariot, ChariotView chariotView)
        {
            var state = new BattleState
            {
                m_chariot = chariot,
                m_chariotView = chariotView,
            };
            return state;
        }
    }
}