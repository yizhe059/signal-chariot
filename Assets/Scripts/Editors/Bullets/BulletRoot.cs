using SetUps;
using UnityEngine;
using Utils.Common;

#if UNITY_EDITOR
using UnityEditor;

namespace Editors.Bullets
{
    public class BulletRoot : MonoSingleton<BulletRoot>
    {
        public SetUp setUp;
        
        [ContextMenu("Save Asset")]
        public void SaveAssets()
        {
            setUp.bulletLibrary.Clear();
            var bulletEdits = gameObject.GetComponentsInChildren<BulletEdit>();
            
            foreach (var edit in bulletEdits)
            {
                setUp.bulletLibrary.Add(edit.CreateBulletSetUp());
            }

            // TODO for loop effect edits
        }
    }
}
#endif