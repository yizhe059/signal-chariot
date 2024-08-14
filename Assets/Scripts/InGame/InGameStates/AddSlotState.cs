using System.Collections.Generic;
using InGame.Boards;
using InGame.Cores;
using InGame.Views;
using InGame.UI;
using UnityEngine;

namespace InGame.InGameStates
{
    public class AddSlotState: InGameState
    {
        public override InGameStateType type => InGameStateType.AddSlotState;
        private BoardView m_boardView;
        private Board m_board;
        private int m_amount;
        private int m_currentSelectableAmount;
        private List<BoardPosition> m_selectableSlots;
            
        public override void Enter(InGameState last)
        {
            Debug.Log("Enter AddSlot");
            var cameraManager = GameManager.Instance.GetCameraManager();
            cameraManager.BoardCameraSetActive(true);
            cameraManager.MiniBoardCameraSetActive(false);
            cameraManager.BattleCameraSetActive(false);
            
            // change all the adjacent slot to selectable
            m_selectableSlots = m_board.GetAdjacentSlots();

            foreach (var pos in m_selectableSlots)
            {
                m_board.SetSlotStatus(pos, SlotStatus.Selectable);
            }
            
            m_currentSelectableAmount = m_selectableSlots.Count;
            if (m_amount == 0) GameManager.Instance.ChangeToNullState();
            
            // register the on click event for the board
            var boardCamera = GameManager.Instance.GetCameraManager().boardCamera;
            GameManager.Instance.GetInputManager().RegisterClickEvent(boardCamera, OnClicked);

            BattleProgressUI.Instance.Hide();
            BattleResultUI.Instance.Hide();
            AndroidStatusUI.Instance.Show();
            NavigationBarUI.Instance.Show();
            BoardBarUI.Instance.Show();
        }

        public override void Exit()
        {
            var boardCamera = GameManager.Instance.GetCameraManager().boardCamera;
            GameManager.Instance.GetInputManager().UnregisterClickEvent(boardCamera, OnClicked);
            
            foreach (var pos in m_selectableSlots)
            {
                if(m_board.GetSlotStatus(pos)== SlotStatus.Selectable)
                    m_board.SetSlotStatus(pos, SlotStatus.Hidden);
            }
            
            m_selectableSlots.Clear();
            Debug.Log("Exit AddSlot");

            BattleProgressUI.Instance.Show();
            BattleResultUI.Instance.Show();
            AndroidStatusUI.Instance.Hide();
            NavigationBarUI.Instance.Hide();
            BoardBarUI.Instance.Hide();
        }

        private void OnClicked(Vector2 worldPosition)
        {
            if (!m_boardView.GetXY(worldPosition, out int x, out int y)) return;

            if (m_board.GetSlotStatus(x, y) == SlotStatus.Selectable)
            {
                m_board.SetSlotStatus(x, y, SlotStatus.Empty);
                m_amount--;
                m_currentSelectableAmount--;
                
                var lists = m_board.GetAdjacentSlots(x, y);

                
                foreach (var pos in lists)
                {
                    m_board.SetSlotStatus(pos, SlotStatus.Selectable);
                    m_selectableSlots.Add(pos);
                    m_currentSelectableAmount++;
                }
                
                if (m_amount <= 0 || m_currentSelectableAmount <= 0)
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