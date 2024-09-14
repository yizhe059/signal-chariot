using InGame.Boards.Modules;
using InGame.Views;
using UnityEngine;

namespace InGame.Boards
{
    public class GeneralBoard
    {

        public Board board { get; private set; }
        public Board extraBoard { get; private set; }
        public BoardView boardView { get; private set; }

        public bool AddModule(Module module, out BoardPosition pos)
        {
            if (extraBoard.PlaceModule(module, out pos.x, out pos.y))
            {
                boardView.CreateModuleView(module, pos, false);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveAllExtraBoardModules()
        {
            extraBoard.DestroyAllModules();
            
        }

        public void Reset()
        {
            board.Reset();
        }
        
        public static GeneralBoard CreateGeneralBoard(Board newBoard, Board newExtraBoard, BoardView newBoardView)
        {
            return new GeneralBoard
            {
                board = newBoard,
                extraBoard = newExtraBoard,
                boardView = newBoardView
            };
        }
        
        
    }
}