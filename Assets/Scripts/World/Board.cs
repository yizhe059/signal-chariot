using UnityEngine;
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
    
    [System.Serializable]
    public class Board : Grid<Slot>
    {

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