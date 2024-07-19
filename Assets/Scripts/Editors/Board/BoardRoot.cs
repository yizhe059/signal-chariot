#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using World;

namespace Editors.Board
{
    public class BoardRoot: MonoBehaviour
    {
        public SetUp setUp;
        
        [ContextMenu("Save Asset")]
        public void SaveAssets()
        {
            var board= transform.Find("Board").GetComponent<BoardEdit>();
            setUp.boardSetUp = board.GenerateBoardSetUp();
            EditorUtility.SetDirty(setUp);
            Debug.Log("Save Asset");

        }
    }
}

#endif