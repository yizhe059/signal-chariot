using InGame.Boards;
using InGame.Boards.Signals;

namespace SetUps
{
    public struct SignalSetUp
    {
        public int id;

        public SignalType type;
        
        public Signal.Direction dir;

        public int energy;

        public BoardPosition pos;
    }
}