using System.Collections.Generic;
using UnityEngine;
using World.Cores;
using World.Views;

namespace World.InGameStates
{
    public class AddSlotState: InGameState
    {
        public override InGameStateType type => InGameStateType.AddSlotState;
        private BoardView m_boardView;
        private Board m_board;
        private int m_amount;
        private List<BoardPosition> m_selectableSlots;
            
        public override void Enter(InGameState last)
        {
            // change all the adjacent slot to selectable
            m_selectableSlots = m_board.GetAdjacentSlots();

            foreach (var pos in m_selectableSlots)
            {
                m_board.SetSlotStatus(pos, SlotStatus.Selectable);
            }

            // register the on click event for the board
            GameManager.Instance.GetInputManager().RegisterClickEvent(OnClicked);

        }

        public override void Exit()
        {
            GameManager.Instance.GetInputManager().UnregisterClickEvent(OnClicked);
            
            foreach (var pos in m_selectableSlots)
            {
                if(m_board.GetSlotStatus(pos)== SlotStatus.Selectable)
                    m_board.SetSlotStatus(pos, SlotStatus.Hidden);
            }
            
            m_selectableSlots.Clear();
        }

        private void OnClicked(Vector2 worldPosition)
        {
            if (!m_boardView.GetXY(worldPosition, out int x, out int y)) return;

            if (m_board.GetSlotStatus(x, y) == SlotStatus.Selectable)
            {
                m_board.SetSlotStatus(x, y, SlotStatus.Empty);
                m_amount--;
                var lists = m_board.GetAdjacentSlots(x, y);

                
                foreach (var pos in lists)
                {
                    m_board.SetSlotStatus(pos, SlotStatus.Selectable);
                    m_selectableSlots.Add(pos);
                }

                if (m_amount <= 0)
                {
                    GameManager.Instance.ChangeToNullState();
                }
            }
        }

        public static AddSlotState CreateAddSlotState(BoardView boardView, Board board, int amount)
        {
            Debug.Assert(amount > 0, "amount must be greater than 0");
            var state = new AddSlotState
            {
                m_boardView = boardView,
                m_board = board,
                m_amount = amount
            };
            

            return state;
        }
    }
}