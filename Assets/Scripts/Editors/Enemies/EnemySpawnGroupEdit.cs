using System.Collections.Generic;
using SetUps;
using UnityEngine;
using UnityEngine.Serialization;

#if UNITY_EDITOR
namespace Editors.Enemies
{    
    public class EnemySpawnGroupEdit: MonoBehaviour
    {
        public EnemyPlainGroupSpawnBlk group;
        public void OnValidate()
        {

            var enemyLib = EnemyRoot.Instance.setUp.enemyLibrary;
            if (group.enemiesPool == null) return;
            foreach (var blk in group.enemiesPool)
            {
                
                if (blk.enemyID >= 0 && blk.enemyID < enemyLib.Count)
                {
                    blk.enemyName = enemyLib[blk.enemyID].name;
                }
                else
                {
                    blk.enemyName = "Invalid ID";
                }

            }
            
            

        }

        public EnemyPlainGroupSpawnBlk CreateSpawnGroupBlks()
        {
            var newGroup = group;
            newGroup.enemiesPool = new List<EnemySpawningBlock>();

            foreach (var blk in group.enemiesPool)
            {
                newGroup.enemiesPool.Add(new EnemySpawningBlock(blk));
            }

            return newGroup;
        }
    }
}
#endif