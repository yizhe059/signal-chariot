using Utils.Common;
using World;

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