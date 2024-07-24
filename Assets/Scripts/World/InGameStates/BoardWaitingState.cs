using UnityEngine;
using World.Cores;
using World.Views;

namespace World.InGameStates
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
            if (!m_boardView.GetXY(worldPosition, out int x, out int y)) return;

            if (m_board.GetSlotStatus(x, y) == SlotStatus.Occupied)
            {
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