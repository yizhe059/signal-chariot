using System.Collections.Generic;
using UnityEngine;
using Utils.Common;
using World;

namespace Editors.Board
{
    
    public class BoardEdit: MonoBehaviour
    {
        [Min(1)]
        public int width, height;
        public Vector3 originPosition;
        public float cellSize;
        public List<BoardPosition> openSlots = new List<BoardPosition>();
        private Grid<SlotEdit> m_board;

        private float m_prevCellSize=-1;
        private int m_prevWidth=-1, m_prevHeight=-1;
        private Vector3 m_prevOriginPosition;
        private List<BoardPosition> prevOpenSlots = new List<BoardPosition>();
        private bool m_firstTime = true;
        public void OnValidate()
        {
            
            if (m_prevCellSize != cellSize || m_prevWidth != width || m_prevHeight != height || m_prevOriginPosition != originPosition)
            {

                m_board = new Grid<SlotEdit>(width, height, cellSize, originPosition, () => new SlotEdit());
            }


            foreach (var pos in prevOpenSlots)
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
            prevOpenSlots.Clear();
            foreach (var pos in openSlots)
            {
                prevOpenSlots.Add(pos);
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

                    Gizmos.color = Color.cyan;
                    
                    Gizmos.DrawLine(bl, br);
                    Gizmos.DrawLine(br, tr);
                    Gizmos.DrawLine(tr, tl);
                    Gizmos.DrawLine(tl, bl);

                    var slot = m_board.GetValue(x, y);

                    if (slot == null) return;
                    if (slot.status == SlotStatus.Hidden)
                    {
                        Gizmos.color = Color.grey;
                        
                    }else if (slot.status == SlotStatus.Empty)
                    {
                        Gizmos.color = Color.white;
                    }
                    Gizmos.DrawCube((bl + tr) /2 + Vector3.forward , m_board.cellSize * new Vector3(1, 1, 1));
                }
            }
        }
    }
}