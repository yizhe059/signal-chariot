using System;
using InGame.BattleFields.Enemies;
using SetUps;
using UnityEngine;

namespace Editors.Enemies
{
    public class EnemySpawnGroupEdit: MonoBehaviour
    {
        [Min(0f)]
        public float start, end;
        public int enemyID;
        public string enemyName;
        [Min(1)]
        public int count;

        public SpawnLogic spawnLogic;

        private int prevID = -1; 
        public void OnValidate()
        {

            var enemyLib = EnemyRoot.Instance.setUp.enemyLibrary;

            if (enemyID >= 0 && enemyID < enemyLib.Count)
            {
                enemyName = enemyLib[enemyID].name;
            }
            else
            {
                enemyName = "Invalid ID";
            }

            if (end < start) end = start;

        }

        public EnemySpawnGroupBlk CreateSpawnGroupBlk()
        {
            return new EnemySpawnGroupBlk
            {
                range = new SpawningRange { start = start, end = end },
                spawningBlock = new EnemySpawningBlock { count = count, enemyID = enemyID },
                spawnLogic = spawnLogic
            };
        }
    }
}