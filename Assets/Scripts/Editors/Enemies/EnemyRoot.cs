using SetUps;
using UnityEngine;
using Utils.Common;

#if UNITY_EDITOR
using UnityEditor;

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
            
            setUp.enemySpawnSetUp.enemySpawnGroups.Clear();
            var groupEdit= transform.GetComponentInChildren<EnemyGroupsEdit>();
            var list = groupEdit.GetBlks();
            foreach (var group in list)
            {
                setUp.enemySpawnSetUp.enemySpawnGroups.Add(group);
            }
            
            setUp.enemySpawnSetUp.enemySpawnWaves.Clear();
            var waveEdit= transform.GetComponentInChildren<EnemyWavesEdit>();
            var waveList = waveEdit.GetBlks();
            foreach (var wave in waveList)
            {
                setUp.enemySpawnSetUp.enemySpawnWaves.Add(wave);
            }
            
            setUp.enemySpawnSetUp.enemySpawnLevels.Clear();
            var levelEdit= transform.GetComponentInChildren<EnemyLevelsEdit>();
            var levelList = levelEdit.GetBlks();
            foreach (var level in levelList)
            {
                setUp.enemySpawnSetUp.enemySpawnLevels.Add(level);
            }

            EditorUtility.SetDirty(setUp);
            Debug.Log("Save Asset");

        }
    }
}
#endif