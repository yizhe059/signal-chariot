#if UNITY_EDITOR
using SetUps;
using UnityEditor;
using UnityEngine;

namespace Editors.Enemies
{
    public class EnemyRoot: MonoBehaviour
    {
        public SetUp setUp;
        
        [ContextMenu("Save Asset")]
        public void SaveAssets()
        {
            setUp.enemyLibrary.Clear();
            var enemyEdits = transform.GetComponentsInChildren<EnemyEdit>();

            foreach (var edit in enemyEdits)
            {
                setUp.enemyLibrary.Add(edit.CreateEnemySetUp());
            }
            
            EditorUtility.SetDirty(setUp);
            Debug.Log("Save Asset");

        }
    }
}

#endif