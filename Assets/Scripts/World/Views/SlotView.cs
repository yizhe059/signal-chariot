﻿using UnityEngine;

namespace World.Views
{
    public class SlotView : MonoBehaviour
    {
        private BoardPosition m_position;
        private BoardView m_boardView;

        public void OnStatusChanged(SlotStatus status)
        {
            if (status is SlotStatus.Empty or SlotStatus.Occupied)
            {
                gameObject.SetActive(true);
            }
            else if (status == SlotStatus.Hidden)
            {
                gameObject.SetActive(false);
            }
        }
        
        public void Init(BoardView boardView, BoardPosition position, Vector3 worldPosition, SlotStatus status)
        {
            m_boardView = boardView;
            m_position = position;
            transform.localPosition = worldPosition;
            OnStatusChanged(status);
        }
    }
}