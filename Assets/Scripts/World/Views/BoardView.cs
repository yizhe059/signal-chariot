﻿using UnityEngine;
using Utils.Common;
using World.SetUps;

namespace World.Views
{
    public class BoardView: MonoBehaviour
    {
        private Board m_board;
        private SlotView m_slotPrefab;
        private Transform m_horizontalBorderPrefab, m_verticalBorderPrefab;
        private Grid<SlotView> m_slots;
        
        
        public void Init(Board board, BoardSetUp boardSetUp)
        {
            m_board = board;
            m_slotPrefab = boardSetUp.slotPrefab;
            m_horizontalBorderPrefab = boardSetUp.horizontalBorderPrefab;
            m_verticalBorderPrefab = boardSetUp.verticalBorderPrefab;

            m_slots = new Grid<SlotView>(boardSetUp.width, boardSetUp.height, boardSetUp.cellSize,
                boardSetUp.originPosition,
                (g, x, y) => null);
            
            for (int x = 0; x < m_board.width; x++)
            {
                for (int y = 0; y < m_board.height; y++)
                {
                    var slotStatus = board.GetSlotStatus(x, y);
                    var slotView = Instantiate(m_slotPrefab, transform);
                    var boardPosition = new BoardPosition { x = x, y = y };
                    slotView.Init(this, boardPosition, GetSlotCenterWorldPosition(x, y), slotStatus);
                    m_slots.SetValue(x, y, slotView);
                }
            }
            
        }
        
        private Vector3 GetSlotCenterWorldPosition(int x, int y)
        {
            var bl = m_slots.GetWorldPosition(x, y);
            return bl + 0.5f * m_slots.cellSize * new Vector3(1, 1, 0);
        }
        
        
        
    }
}