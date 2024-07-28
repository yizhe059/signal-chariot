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
        private SlotView m_slotPrefab;
        private SignalView m_signalPrefab; 
            
        private Transform m_horizontalBorderPrefab, m_verticalBorderPrefab;
        private Grid<SlotView> m_slots;

        private Transform m_slotTransform, m_modulesTransform, m_signalsTransform;

        public float cellSize => m_slots.cellSize;
        public void Init(Board board, BoardSetUp boardSetUp)
        {
            m_board = board;
            m_slotPrefab = boardSetUp.slotPrefab;
            m_horizontalBorderPrefab = boardSetUp.horizontalBorderPrefab;
            m_verticalBorderPrefab = boardSetUp.verticalBorderPrefab;

            m_slots = new Grid<SlotView>(boardSetUp.width, boardSetUp.height, boardSetUp.cellSize,
                boardSetUp.originPosition,
                (g, x, y) => null);

            m_slotTransform = new GameObject("[Slots]").transform;
            m_slotTransform.parent = transform;
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
            
            m_modulesTransform = new GameObject("[Modules]").transform;
            m_modulesTransform.parent = transform;
            m_modulesTransform.localPosition = Vector3.zero;
            foreach (var activeModules in boardSetUp.modules)
            {
                var pos = activeModules.pos;
                var module = board.GetModuleSlot(pos.x, pos.y).module;
                
                CreateModuleView(module, GetSlotCenterWorldPosition(pos.x, pos.y));
            }
            
            m_board.RegisterStatusEvent(OnSlotStatusChanged);
            
            m_signalsTransform = new GameObject("[Signals]").transform;
            m_signalsTransform.parent = transform;
            m_signalPrefab = boardSetUp.signalPrefab;
            m_signalsTransform.localPosition = Vector3.zero;

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