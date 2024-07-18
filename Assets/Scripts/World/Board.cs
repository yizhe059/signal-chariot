using UnityEngine;
using Utils.Common;

namespace World
{
    [System.Serializable]
    public struct BoardPosition
    {
        public int x;
        public int y;
    }
    
    [System.Serializable]
    public class Board : Grid<Slot>
    {

        private Board(int width, int height, float cellSize, Vector3 originPosition) : base(width, height, cellSize,
            originPosition, () => new Slot())
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    m_gridArray[x,y].SetPosition(x, y);
                }
            }
        }
        
        public static Board GenerateBoard(int width, int height, float cellSize, Vector3 originPosition)
        {
            return new Board(width, height, cellSize, originPosition);
        }

        
    }
}