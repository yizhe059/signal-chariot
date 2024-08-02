#if UNITY_EDITOR
using SetUps;
using UnityEditor;
using UnityEngine;
using Utils.Common;

namespace Editors.Enemies
{
    public class EnemyRoot: MonoSingleton<EnemyRoot>
    {
        public SetUp setUp;
        
        [ContextMenu("Save Asset")]
        public void SaveAssets()
        {
            setUp.enemyLibrary.Clear();
            var enemyLib = transform.Find("EnemyLib");
            var enemyEdits = enemyLib.GetComponentsInChildren<EnemyEdit>();

            foreach (var edit in enemyEdits)
            {
                setUp.enemyLibrary.Add(edit.CreateEnemySetUp());
            }
            
            setUp.enemySpawns.Clear();
            var enemySpawnEdit = transform.GetComponentInChildren<EnemySpawnEdit>();
            var list = enemySpawnEdit.CreateSetUp();
            foreach (var spawnSetUp in list)
            {
                setUp.enemySpawns.Add(spawnSetUp);
            }
            EditorUtility.SetDirty(setUp);
            Debug.Log("Save Asset");

        }
    }
}

#endif