namespace World
{
    public struct BoardPosition
    {
        public int x;
        public int y;
    }
    
    public class Board
    {
        private int m_width, m_height;
        private Slot[,] m_slots;
        
        public static Board GenerateBoard(int width, int height)
        {
            return new Board
            {
                m_width = width,
                m_height = height,
                m_slots = new Slot[height, width]
            };
        }
    }
}