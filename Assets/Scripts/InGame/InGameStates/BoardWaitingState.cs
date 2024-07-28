using InGame.Boards;
using InGame.Cores;
using InGame.Views;
using UnityEngine;

namespace InGame.InGameStates
{
    public class BoardWaitingState: InGameState
    {
        public override InGameStateType type => InGameStateType.BoardWaitingState;
        private BoardView m_boardView;
        private Board m_board;

        public override void Enter(InGameState last)
        {
            Debug.Log("Enter board waiting");
            GameManager.Instance.GetInputManager().RegisterClickEvent(OnClicked);
        }

        public override void Exit()
        {
            GameManager.Instance.GetInputManager().UnregisterClickEvent(OnClicked);
            Debug.Log("Exit board waiting");
        }


        private void OnClicked(Vector2 worldPosition)
        {
            Debug.Log("hello1");
            
            if (!m_boardView.GetXY(worldPosition, out int x, out int y)) return;
            
            Debug.Log("hello2");
            
            if (m_board.GetSlotStatus(x, y) == SlotStatus.Occupied)
            {
                Debug.Log("hello3");
                var module = m_board.RemoveModule(x, y);

                GameManager.Instance.ChangeToModulePlacingState(module);
            }
        }

        public static BoardWaitingState CreateState(Board board, BoardView boardView)
        {
            var state = new BoardWaitingState
            {
                m_board = board,
                m_boardView = boardView,

            };
            return state;
        }
    }
}