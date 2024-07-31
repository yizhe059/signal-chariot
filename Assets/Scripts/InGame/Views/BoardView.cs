using InGame.Boards;
using InGame.Boards.Modules;
using InGame.Boards.Signals;
using SetUps;
using UnityEngine;
using Utils;
using Utils.Common;

namespace InGame.Views
{
    
    //TO DO: Should the baord view be monobehavior?
    public class BoardView: MonoBehaviour
    {
        private Board m_board;
        private Board m_extraBoard;
        private SlotView m_slotPrefab;
        private SignalView m_signalPrefab; 
            
        private Transform m_horizontalBorderPrefab, m_verticalBorderPrefab;
        private Grid<SlotView> m_slots;
        private Grid<SlotView> m_extraSlots;

        private Transform m_slotTransform, m_modulesTransform, m_signalsTransform;

        public float cellSize => m_slots.cellSize;
        public void Init(Board board, Board extraBoard, BoardSetUp boardSetUp, BoardSetUp extraBoardSetup)
        {
            m_board = board;
            m_extraBoard = extraBoard;
            
            m_slotPrefab = boardSetUp.slotPrefab;
            m_horizontalBorderPrefab = boardSetUp.horizontalBorderPrefab;
            m_verticalBorderPrefab = boardSetUp.verticalBorderPrefab;
            
            #region NormalBoardSetUp
            m_slots = new Grid<SlotView>(boardSetUp.width, boardSetUp.height, boardSetUp.cellSize,
                boardSetUp.originPosition,
                (g, x, y) => null);

            var normalBoardTransform = new GameObject("[NormalBoard]").transform;
            normalBoardTransform.parent = transform;
            normalBoardTransform.localPosition = Vector3.zero;

            m_slotTransform = new GameObject("[Slots]").transform;
            m_slotTransform.parent = normalBoardTransform;
            m_slotTransform.localPosition = Vector3.zero;
            for (int x = 0; x < m_board.width; x++)
            {
                for (int y = 0; y < m_board.height; y++)
                {
                    var slotStatus = board.GetSlotStatus(x, y);
                    var slotView = Instantiate(m_slotPrefab, m_slotTransform);
                    var boardPosition = new BoardPosition { x = x, y = y };
                    var worldPos = GetSlotCenterWorldPosition(x, y);
                    worldPos.z = Constants.SLOT_DEPTH;
                    slotView.Init(this, boardPosition, worldPos, slotStatus);
                    m_slots.SetValue(x, y, slotView);
                }
            }
            
            
            
            m_board.RegisterStatusEvent(OnSlotStatusChanged);
            #endregion
            
            #region Signals
            m_signalsTransform = new GameObject("[Signals]").transform;
            m_signalsTransform.parent = normalBoardTransform;
            m_signalPrefab = boardSetUp.signalPrefab;
            m_signalsTransform.localPosition = Vector3.zero;
            #endregion
            
            #region ExtraBoardSetUp
            var extraBoardTransform = new GameObject("[ExtraBoard]").transform;
            extraBoardTransform.parent = transform;
            extraBoardTransform.localPosition = Vector3.zero;
            
            var extraSlotsTransform = new GameObject("[ExtraSlots]").transform;
            extraSlotsTransform.parent = extraBoardTransform;
            extraSlotsTransform.localPosition = Vector3.zero;
            
            m_extraSlots = new Grid<SlotView>(extraBoardSetup.width, extraBoardSetup.height, extraBoardSetup.cellSize,
                extraBoardSetup.originPosition,
                (g, x, y) => null);
            for (int x = 0; x < m_extraBoard.width; x++)
            {
                for (int y = 0; y < m_extraBoard.height; y++)
                {
                    var slotStatus = m_extraBoard.GetSlotStatus(x, y);
                    var slotView = Instantiate(m_slotPrefab, extraSlotsTransform);
                    var boardPosition = new BoardPosition { x = x, y = y };
                    var worldPos = GetSlotCenterWorldPosition(x, y, m_extraSlots);
                    worldPos.z = Constants.SLOT_DEPTH;
                    slotView.Init(this, boardPosition, worldPos, slotStatus);
                    m_extraSlots.SetValue(x, y, slotView);
                }
            }
            #endregion
            
            #region Modules
            m_modulesTransform = new GameObject("[Modules]").transform;
            m_modulesTransform.parent = transform;
            m_modulesTransform.localPosition = Vector3.zero;
            foreach (var activeModules in boardSetUp.modules)
            {
                var pos = activeModules.pos;
                var module = board.GetModuleSlot(pos.x, pos.y).module;
                
                CreateModuleView(module, GetSlotCenterWorldPosition(pos.x, pos.y, m_slots));
            }
            
            foreach (var activeModules in extraBoardSetup.modules)
            {
                var pos = activeModules.pos;
                var module = extraBoard.GetModuleSlot(pos.x, pos.y).module;
                
                CreateModuleView(module, GetSlotCenterWorldPosition(pos.x, pos.y, m_extraSlots));
            }
            
            #endregion
        }

        public void CreateModuleView(Module module, Vector3 pos)
        {
            pos.z = Constants.MODULE_DEPTH;
            var moduleView = module.CreateModuleView(m_modulesTransform);
            moduleView.SetWorldPos(pos);
        }
        
        private Vector3 GetSlotCenterWorldPosition(int x, int y)
        {
            var bl = m_slots.GetWorldPosition(x, y);
            return bl + 0.5f * m_slots.cellSize * new Vector3(1, 1, 0);
        }

        public Vector3 GetSlotCenterWorldPosition(BoardPosition boardPos)
        {
            return GetSlotCenterWorldPosition(boardPos.x, boardPos.y);
        }
        
        public Vector3 GetSlotCenterWorldPosition(BoardPosition boardPos, bool isNormal)
        {
            return GetSlotCenterWorldPosition(boardPos.x, boardPos.y, isNormal ? m_slots : m_extraSlots);
        }

        private Vector3 GetSlotCenterWorldPosition(int x, int y, Grid<SlotView> slots)
        {
            var bl = slots.GetWorldPosition(x, y);
            return bl + 0.5f * m_slots.cellSize * new Vector3(1, 1, 0);
        }
        
        private Vector3 GetSlotCenterWorldPosition(BoardPosition boardPos, Grid<SlotView> slots)
        {
            return GetSlotCenterWorldPosition(boardPos.x, boardPos.y, slots);
        }

        public void GetActiveBoardCornerPos(out Vector3 minPos, out Vector3 maxPos)
        {
            BoardPosition min = new BoardPosition(m_slots.width, m_slots.height);
            BoardPosition max = new BoardPosition(-1, -1);
            
            for (int x = 0; x < m_slots.width; x++)
            {
                for (int y = 0; y < m_slots.height; y++)
                {
                    var status = m_board.GetSlotStatus(x, y);
                    if (status is SlotStatus.Empty or SlotStatus.Occupied)
                    {
                        if (x < min.x) min.x = x;
                        if (y < min.y) min.y = y;
                        if (x > max.x) max.x = x;
                        if (y > max.y) max.y = y;

                    }
                }
            }

            maxPos = m_slots.GetWorldPosition(max.x, max.y) + new Vector3(1,1,0) * m_slots.cellSize;
            minPos = m_slots.GetWorldPosition(min.x, min.y);
            
        }
        
        private void OnSlotStatusChanged(int x, int y, SlotStatus status)
        {
            m_slots.GetValue(x, y).OnStatusChanged(status);
        }

        public bool GetXY(Vector2 worldPosition, out int x, out int y)
        {
            Vector2 boardWorldPosition = transform.position;
            Vector2 localPosition = worldPosition - boardWorldPosition;
            m_slots.GetXY(localPosition, out x, out y);
            
            if ((x >= 0 && x < m_slots.width) && (y >= 0 && y < m_slots.height))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool GetXY(Vector2 worldPosition, out int x, out int y, out bool isNormal)
        {
            Vector2 boardWorldPosition = transform.position;
            Vector2 localPosition = worldPosition - boardWorldPosition;
            m_slots.GetXY(localPosition, out x, out y);
            
            if ((x >= 0 && x < m_slots.width) && (y >= 0 && y < m_slots.height))
            {
                isNormal = true;
                return true;
            }
            else
            {
                m_extraSlots.GetXY(localPosition, out x, out y);
                if ((x >= 0 && x < m_extraSlots.width) && (y >= 0 && y < m_extraSlots.height))
                {
                    isNormal = false;
                    return true;
                }
                else
                {
                    isNormal = false;
                    return false;
                }
                
            }
        }
        
        public bool GetBoardPosition(Vector2 worldPosition, out BoardPosition boardPos)
        {
            Vector2 boardWorldPosition = transform.position;
            Vector2 localPosition = worldPosition - boardWorldPosition;
            m_slots.GetXY(worldPosition, out int x, out int y);
            boardPos.x = x;
            boardPos.y = y;
            
            if ((x >= 0 && x < m_slots.width) && (y >= 0 && y < m_slots.height))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SignalView CreateSignalView(Signal signal)
        {
            return SignalView.CreateSignalView(m_signalPrefab, m_signalsTransform, this, signal);
        }

        public void DestroySignalView(Signal signal)
        {
            var view = signal.view;
            view.SelfDestroy();
        }
        
        
    }
}