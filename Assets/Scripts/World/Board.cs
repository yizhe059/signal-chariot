using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils.Common;
using World.SetUps;

namespace World
{
    [System.Serializable]
    public struct BoardPosition
    {
        public int x;
        public int y;

        public override string ToString()
        {
            return $"{x} , {y}";
        }
    }
    
    //To DO: 可能Board的逻辑是不需要position的，只有boardView才需要，在board里加位置是redundant的
    [System.Serializable]
    public class Board : Grid<Slot>
    {
        private UnityEvent<int, int, SlotStatus> m_onStatusChanged = new UnityEvent<int, int, SlotStatus>();

        #region Constructors
        private Board(int width, int height, float cellSize, Vector3 originPosition) : base(width, height, cellSize,
            originPosition, (grid, x, y) => Slot.GenerateSlot(x, y))
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    m_gridArray[x,y].SetPosition(x, y);
                }
            }
        }

        private Board(Board other): base(other.m_width, other.m_height, other.m_cellSize, other.m_originPosition,
            (grid, x, y) => Slot.GenerateSlot(x, y))
        {
            for (int x = 0; x < m_width; x++)
            {
                for (int y = 0; y < m_height; y++)
                {
                    if (other.GetValue(x, y) == null)
                    {
                        Debug.LogError($"{x}, {y} is null");
                    }
                    else
                    {
                        SetSlotStatus(x, y, other.GetSlotStatus(x, y));
                    }
                    
                }
            }
        }

        public Board(BoardSetUp setUp) : base(setUp.width, setUp.height, setUp.cellSize, setUp.originPosition,
            (grid, x, y) => Slot.GenerateSlot(x, y))
        {
            foreach (var pos in setUp.openSlots)
            {
                SetSlotStatus(pos, SlotStatus.Empty);
            }
        }
        
        public static Board GenerateBoard(int width, int height, float cellSize, Vector3 originPosition)
        {
            return new Board(width, height, cellSize, originPosition);
        }

        #endregion
        
        #region SlotStatus
        public void SetSlotStatus(int x, int y, SlotStatus status)
        {
            Slot slot = GetValue(x, y);
            if (slot != null)
            {
                slot.status = status;
                m_onStatusChanged.Invoke(x, y, status);
            }
        }

        public void SetSlotStatus(BoardPosition pos, SlotStatus status) => SetSlotStatus(pos.x, pos.y, status);

        public SlotStatus GetSlotStatus(int x, int y)
        {
            Slot slot = GetValue(x, y);
            if (slot != null)
            {
                return slot.status;
            }
            else
            {
                return SlotStatus.Hidden;
            }
        }

        public bool GetSlotStatus(int x, int y, out SlotStatus status)
        {
            Slot slot = GetValue(x, y);
            if (slot != null)
            {
                status = slot.status;
                return true;
            }
            else
            {
                status = SlotStatus.Hidden;
                return false;
            }
        }

        public SlotStatus GetSlotStatus(BoardPosition pos) => GetSlotStatus(pos.x, pos.y);
        #endregion
        
        #region Events
        public void RegisterStatusEvent(UnityAction<int, int, SlotStatus> act)
        {
            m_onStatusChanged.AddListener(act);
            for (int x = 0; x < m_width; x++)
            {
                for (int y = 0; y < m_height; y++)
                {
                    m_onStatusChanged.Invoke(x, y, GetSlotStatus(x, y));
                }
            }
        }
        
        public void UnregisterStatusEvent(UnityAction<int, int, SlotStatus> act)
        {
            m_onStatusChanged.RemoveListener(act);

        }
        #endregion

        private bool IsAdjacentSlot(int x, int y)
        {
            if (!GetSlotStatus(x, y, out SlotStatus status) || status != SlotStatus.Hidden)
            {
                return false;
            }
            
            for (int deltaX = -1; deltaX <= 1; deltaX++)
            {
                for (int deltaY = -1; deltaY <= 1; deltaY++)
                {
                    // 只有上下左右才是相邻的格子，左上不算
                    if (Mathf.Abs(deltaX) + Mathf.Abs(deltaY) != 1) continue;


                    if (GetSlotStatus(x + deltaX, y + deltaY, out SlotStatus adjStatus))
                    {
                        if (adjStatus == SlotStatus.Occupied || adjStatus == SlotStatus.Empty)
                        {
                            return true;
                        }
                    }
                            
                }
            }

            return false;
        }
        
        /// <summary>
        /// Get All adjacent slots in the board
        /// </summary>
        /// <returns></returns>
        public List<BoardPosition> GetAdjacentSlots()
        {
            var adjacentSlot = new List<BoardPosition>();

            for (int x = 0; x < m_width; x++)
            {
                for (int y = 0; y < m_height; y++)
                {
                    if (IsAdjacentSlot(x, y))
                    {
                        adjacentSlot.Add(new BoardPosition { x = x, y = y });
                    }
                    
                    
                }
            }
            return adjacentSlot;
        }
        
        /// <summary>
        /// Get all the adjacent slot of a single slot
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public List<BoardPosition> GetAdjacentSlots(int x, int y)
        {
            var adjacentSlot = new List<BoardPosition>();

            for (int deltaX = -1; deltaX <= 1; deltaX++)
            {
                for (int deltaY = -1; deltaY <= 1; deltaY++)
                {
                    // 只有上下左右才是相邻的格子，左上不算
                    if (Mathf.Abs(deltaX) + Mathf.Abs(deltaY) != 1) continue;


                    if (GetSlotStatus(x + deltaX, y + deltaY, out SlotStatus adjStatus))
                    {
                        if (adjStatus == SlotStatus.Hidden)
                        {
                            adjacentSlot.Add(new BoardPosition { x = x + deltaX, y = y + deltaY });
                        }
                    }
                            
                }
            }
            return adjacentSlot;
        }
        
        public override string ToString()
        {
            string result = $"width: {width}, height: {height}, cell size: {cellSize}, Empty Slots: ";

            for (int x = 0; x < m_width; x++)
            {
                for (int y = 0; y < m_height; y++)
                {
                    if (GetSlotStatus(x, y) == SlotStatus.Empty)
                    {
                        result += $"({x}, {y}), ";
                    }
                }
            }
            return result;
        }
    }
}