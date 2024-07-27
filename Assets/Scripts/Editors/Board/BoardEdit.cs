using System.Collections.Generic;
using InGame.Boards;
using InGame.Boards.Modules;
using InGame.Views;
using SetUps;
using UnityEngine;
using Utils.Common;

namespace Editors.Board
{
    
    public class BoardEdit: MonoBehaviour
    {
        [Min(1)]
        public int width, height;
        public Vector3 originPosition;
        public float cellSize;
        public List<BoardPosition> openSlots = new List<BoardPosition>();
        public List<ActiveModule> modules = new List<ActiveModule>();

        public SignalView signalPrefab;
        public SlotView slotPrefab;
        public Transform horizontalBorderPrefab, verticalBorderPrefab;
        
        private Grid<SlotEdit> m_board;
        
        private float m_prevCellSize=-1;
        private int m_prevWidth=-1, m_prevHeight=-1;
        private Vector3 m_prevOriginPosition;
        private List<BoardPosition> m_prevOpenSlots = new List<BoardPosition>();

        public void OnValidate()
        {
            
            if (m_prevCellSize != cellSize || m_prevWidth != width || m_prevHeight != height || m_prevOriginPosition != originPosition)
            {

                m_board = new Grid<SlotEdit>(width, height, cellSize, originPosition, 
                    (Grid<SlotEdit> grid, int x, int y) => new SlotEdit(grid, x, y));
            }


            foreach (var pos in m_prevOpenSlots)
            {
                var slot = m_board.GetValue(pos.x, pos.y);
                if (slot != null)
                    slot.status = SlotStatus.Hidden;
            }
            foreach (var pos in openSlots)
            {
                var slot = m_board.GetValue(pos.x, pos.y);
                if (slot != null)
                    slot.status = SlotStatus.Empty;
            }
            
            m_prevCellSize = cellSize;
            m_prevWidth = width;
            m_prevHeight = height;
            m_prevOriginPosition = originPosition;
            m_prevOpenSlots.Clear();
            foreach (var pos in openSlots)
            {
                m_prevOpenSlots.Add(pos);
            }

            foreach (var module in modules)
            {
                int idx = module.moduleID;
                if (idx >= 0 && idx < BoardRoot.Instance.setUp.moduleLibrary.Count)
                    module.moduleName = BoardRoot.Instance.setUp.moduleLibrary[idx].name;
                else
                    module.moduleName = "Invalid ID";
            }
        }

        private void OnDrawGizmos()
        {
            if (m_board == null) return;

            for (int x = 0; x < m_board.width; x++)
            {
                
                for (int y = 0; y < m_board.height; y++)
                {
                    
                    var bl = m_board.GetWorldPosition(x, y);
                    var br = bl + Vector3.right * m_board.cellSize;
                    var tr = br + Vector3.up * m_board.cellSize;
                    var tl = bl + Vector3.up * m_board.cellSize;

                    // Gizmos.color = Color.cyan;
                    //
                    // Gizmos.DrawLine(bl, br);
                    // Gizmos.DrawLine(br, tr);
                    // Gizmos.DrawLine(tr, tl);
                    // Gizmos.DrawLine(tl, bl);

                    var slot = m_board.GetValue(x, y);

                    if (slot == null) return;
                    if (slot.status == SlotStatus.Hidden)
                    {
                        Gizmos.color = Color.grey;
                        
                    }else if (slot.status == SlotStatus.Empty)
                    {
                        Gizmos.color = Color.white;
                    }
                    Gizmos.DrawCube((bl + tr) /2 + Vector3.forward , m_board.cellSize * 0.99f * new Vector3(1, 1, 1));
                }
            }

            var moduleLib = BoardRoot.Instance.setUp.moduleLibrary;
            Gizmos.color= Color.blue;
            foreach (var module in modules)
            {
                Vector3 worldPos = m_board.GetWorldPosition(module.pos.x, module.pos.y);
                Gizmos.DrawCube(worldPos + 1.1f * Vector3.forward + new Vector3(0.5f, 0.5f) , m_board.cellSize * new Vector3(1, 1, 1));
                if (module.moduleID >= moduleLib.Count) continue;
                foreach (var modulePos in BoardRoot.Instance.setUp.moduleLibrary[module.moduleID].otherPositions)
                {
                    var rotatedPos = Module.GetRotatedPos(modulePos, module.orientation);
                    Vector3 otherWorldPos = m_board.GetWorldPosition(module.pos.x + rotatedPos.x, module.pos.y + rotatedPos.y);
                    Gizmos.DrawCube(otherWorldPos + 1.1f * Vector3.forward + new Vector3(0.5f, 0.5f) , m_board.cellSize * new Vector3(1, 1, 1));
                }
            }
        }

        public BoardSetUp GenerateBoardSetUp()
        {
            var setUp = new BoardSetUp
            {
                width = this.width,
                height = this.height,
                cellSize = this.cellSize,
                originPosition = this.originPosition,
                openSlots = new List<BoardPosition>(this.openSlots),
                modules = new List<ActiveModule>(this.modules),
                signalPrefab = this.signalPrefab,
                slotPrefab = this.slotPrefab,
                horizontalBorderPrefab = this.horizontalBorderPrefab,
                verticalBorderPrefab = this.verticalBorderPrefab
            };
            

            return setUp;
        }
    }
}