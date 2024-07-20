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

        public SlotStatus GetSlotStatus(BoardPosition pos) => GetSlotStatus(pos.x, pos.y);

        public Vector3 GetSlotCenterWorldPosition(int x, int y)
        {
            var bl = GetWorldPosition(x, y);
            return bl + 0.5f * m_cellSize * new Vector3(1, 1, 0);
        }
        
        public static Board GenerateBoard(int width, int height, float cellSize, Vector3 originPosition)
        {
            return new Board(width, height, cellSize, originPosition);
        }

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