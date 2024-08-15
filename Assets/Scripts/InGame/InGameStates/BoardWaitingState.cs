using InGame.Boards;
using InGame.Cores;
using InGame.UI;
using InGame.Views;
using UnityEngine;

namespace InGame.InGameStates
{
    public class BoardWaitingState: InGameState
    {
        public override InGameStateType type => InGameStateType.BoardWaitingState;
        private BoardView m_boardView;
        private Board m_board, m_extraBoard;

        public override void Enter(InGameState last)
        {
            Debug.Log("Enter board waiting");
            var cameraManager = GameManager.Instance.GetCameraManager();

            cameraManager.BoardCameraSetActive(true);
            cameraManager.MiniBoardCameraSetActive(false);
            cameraManager.BattleCameraSetActive(false);

            var boardCamera = cameraManager.boardCamera;

            GameManager.Instance.GetInputManager().RegisterClickEvent(boardCamera, OnClicked);
            
            BattleProgressUI.Instance.Hide();
            BattleResultUI.Instance.Hide();
            AndroidStatusUI.Instance.Show();
            NavigationBarUI.Instance.Show();
            BoardBarUI.Instance.Show();
        }

        public override void Exit()
        {
            Debug.Log("Exit board waiting");
            var boardCamera = GameManager.Instance.GetCameraManager().boardCamera;
            GameManager.Instance.GetInputManager().UnregisterClickEvent(boardCamera, OnClicked);
            
            BattleProgressUI.Instance.Show();
            BattleResultUI.Instance.Show();
            AndroidStatusUI.Instance.Hide();
            NavigationBarUI.Instance.Hide();
            BoardBarUI.Instance.Hide();
        }

        private void OnClicked(Vector2 worldPosition)
        {
            if (!m_boardView.GetXY(worldPosition, out int x, out int y, out bool isNormal)) return;

            var board = isNormal ? m_board : m_extraBoard;
            if (board.GetSlotStatus(x, y) == SlotStatus.Occupied)
            {
                var module = board.RemoveModule(x, y);

                GameManager.Instance.ChangeToModulePlacingState(module);
            }
        }

        public static BoardWaitingState CreateState(Board board, Board extraBoard, BoardView boardView)
        {
            var state = new BoardWaitingState
            {
                m_board = board,
                m_extraBoard = extraBoard,
                m_boardView = boardView,
            };
            return state;
        }
    }
}