namespace World
{
    public enum SlotStatus
    {
        Hidden,
        Empty,
        Occupied
    }
    public class Slot
    {
        public SlotStatus status { get; set; }
        private BoardPosition m_position;
        
        //private CellUnit

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