using InGame.Boards;
using Utils.Common;

namespace Editors.Board
{
    public class SlotEdit
    {
        public SlotStatus status;

        public SlotEdit(Grid<SlotEdit> grid, int x, int y)
        {
            status = SlotStatus.Hidden;
        }
    }
}