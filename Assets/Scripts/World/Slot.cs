namespace World
{
    public enum SlotStatus
    {
        Hidden,
        Empty,
        Occupied
    }
    
    [System.Serializable]
    public class Slot
    {
        public SlotStatus status { get; set; } = SlotStatus.Hidden;
        private BoardPosition m_position = new()
        {
            x = 0,
            y = 0
        };

        public void SetPosition(int x, int y)
        {
            m_position = new BoardPosition
            {
                x = x,
                y = y
            };
        }

        public static Slot GenerateSlot(int x, int y, SlotStatus status)
        {
            return new Slot
            {
                m_position = new BoardPosition{x = x, y=y},
                status = status
            };
        }
    }
}