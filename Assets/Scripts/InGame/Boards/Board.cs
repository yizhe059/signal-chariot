using System.Collections.Generic;
using InGame.Boards.Modules;
using InGame.Cores;
using InGame.Effects;
using SetUps;
using UnityEngine;
using UnityEngine.Events;
using Utils.Common;

namespace InGame.Boards
{
    [System.Serializable]
    public struct BoardPosition
    {
        public int x;
        public int y;

        public BoardPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        public BoardPosition(BoardPosition other)
        {
            x = other.x;
            y = other.y;
        }
        
        public static BoardPosition operator+(BoardPosition a, BoardPosition b)
        {
            return new BoardPosition(a.x + b.x, a.y + b.y);
        }
        
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

        private List<Module> m_modules = new List<Module>();

        private bool m_noEffectTrigger = false;
        
        public bool SetNoEffectTrigger(bool val) => m_noEffectTrigger = val;

        #region Constructors

        private Board(int width, int height, float cellSize, Vector3 originPosition) : base(width, height, cellSize,
            originPosition, (grid, x, y) => Slot.GenerateSlot(x, y))
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    m_gridArray[x, y].SetPosition(x, y);
                }
            }
        }

        private Board(Board other) : base(other.m_width, other.m_height, other.m_cellSize, other.m_originPosition,
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

            foreach (var activeModule in setUp.modules)
            {
                var module = GameManager.Instance.GetModuleLib().GenerateModule(activeModule.moduleID);
                module.SetOrientation(activeModule.orientation);
                PlaceModule(module, activeModule.pos);

            }
        }

        public Board(BoardSetUp setUp, SlotStatus status, bool noEffectTrigger): base(setUp.width, setUp.height, setUp.cellSize, setUp.originPosition,
            (grid, x, y) => Slot.GenerateSlot(x, y))
        {
            m_noEffectTrigger = noEffectTrigger;
            
            for (int x = 0; x < m_width; x++)
            {
                for (int y = 0; y < m_height; y++)
                {
                    SetSlotStatus(x, y, status);
                }
            }
            
            foreach (var activeModule in setUp.modules)
            {
                var module = GameManager.Instance.GetModuleLib().GenerateModule(activeModule.moduleID);
                module.SetOrientation(activeModule.orientation);
                PlaceModule(module, activeModule.pos);

            }
        }

        public static Board GenerateBoard(int width, int height, float cellSize, Vector3 originPosition)
        {
            return new Board(width, height, cellSize, originPosition);
        }

        #endregion

        #region Module Realated

        public ModuleSlot GetModuleSlot(int x, int y)
        {
            var slot = GetValue(x, y);
            return slot.moduleSlot;
        }

        public void SetModuleSlot(int x, int y, ModuleSlot moduleSlot)
        {
            var slot = GetValue(x, y);
            slot.moduleSlot = moduleSlot;
        }

        public bool PlaceModule(Module module, BoardPosition pivotPos)
        {
            var slotList = module.GetModuleSlots();

            foreach (var slot in slotList)
            {
                var boardPos = slot.GetBoardPosition(pivotPos);
                if (GetSlotStatus(boardPos) != SlotStatus.Empty) return false;
            }

            foreach (var moduleSlot in slotList)
            {
                var boardPos = moduleSlot.GetBoardPosition(pivotPos);

                SetModuleSlot(boardPos.x, boardPos.y, moduleSlot);
                SetSlotStatus(boardPos.x, boardPos.y, SlotStatus.Occupied);
            }

            if (!m_noEffectTrigger)
            {
                module.TriggerPlacingEffect(new EffectBlackBoard
                {
                    slot = GetValue(pivotPos.x, pivotPos.y),
                    module = module
                });
            }
            

            return true;
        }

        public bool PlaceModule(Module module, out int resultX, out int resultY)
        {
            for (int x = 0; x < m_width; x++)
            {
                for (int y = 0; y < m_height; y++)
                {

                    if (PlaceModule(module, new BoardPosition(x, y)))
                    {
                        resultX = x;
                        resultY = y;
                        return true;
                    }
                }
            }

            resultX = -1;
            resultY = -1;
            return false;
        }
        
        public Module RemoveModule(int x, int y)
        {
            if (GetSlotStatus(x, y) != SlotStatus.Occupied) return null;
            var moduleSlot = GetModuleSlot(x, y);
            var module = moduleSlot.module;

            BoardPosition slotBoardPosition;
            slotBoardPosition.x = x;
            slotBoardPosition.y = y;

            var moduleSlotPos =
                module.GetBoardPositionList(module.GetPivotBoardPosition(moduleSlot, slotBoardPosition));


            foreach (var pos in moduleSlotPos)
            {
                SetModuleSlot(pos.x, pos.y, null);
                SetSlotStatus(pos.x, pos.y, SlotStatus.Empty);
            }

            if (!m_noEffectTrigger)
            {
                module.UnTriggerPlacingEffect(new EffectBlackBoard{slot = GetValue(x, y)});
            }

            

            return module;
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

        public SlotStatus GetSlotStatus(BoardPosition pos) => GetSlotStatus(pos.x, pos.y);

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

        public bool GetSlotStatus(BoardPosition pos, out SlotStatus status) => GetSlotStatus(pos.x, pos.y, out status);

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

        #region Effect

        public void TriggerEffect(int x, int y, EffectBlackBoard blackBoard)
        {
            var slot = GetValue(x, y);
            if (slot == null) return;

            slot.TriggerEffect(blackBoard);
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
            string result = $"width: {width}, height: {height}, cell size: {cellSize}, Slot Status:  ";

            for (int x = 0; x < m_width; x++)
            {
                for (int y = 0; y < m_height; y++)
                {
                    if (GetSlotStatus(x, y) == SlotStatus.Empty)
                    {
                        result += $"({x}, {y}): Empty, ";
                    }else if (GetSlotStatus(x, y) == SlotStatus.Occupied)
                    {
                        result += $"({x}, {y}): Occupied by {GetModuleSlot(x,y).module.name}, ";
                    }
                }
            }
            return result;
        }
    }
}