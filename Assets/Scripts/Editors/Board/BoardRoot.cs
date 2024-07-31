#if UNITY_EDITOR
using SetUps;
using UnityEditor;
using UnityEngine;
using Utils.Common;

namespace Editors.Board
{
    public class BoardRoot: MonoSingleton<BoardRoot>
    {
        public SetUp setUp;
        
        [ContextMenu("Save Asset")]
        public void SaveAssets()
        {
            var board= transform.Find("Board").GetComponent<BoardEdit>();
            setUp.boardSetUp = board.GenerateBoardSetUp();
            
            var extraBoard= transform.Find("ExtraBoard").GetComponent<BoardEdit>();
            setUp.extraBoardSetUp = extraBoard.GenerateBoardSetUp();
            
            // force they all have the same cell size
            setUp.extraBoardSetUp.cellSize = setUp.boardSetUp.cellSize;
            
            EditorUtility.SetDirty(setUp);
            Debug.Log("Save Asset");

        }
    }
}

#endif