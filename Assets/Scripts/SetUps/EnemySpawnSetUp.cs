using System.Collections.Generic;
using InGame.BattleFields.Enemies;

namespace SetUps
{
    [System.Serializable]
    public class EnemySpawnLevelSetUp
    {
        public List<EnemySpawnWaveBlk> waves;
    }
    
    [System.Serializable]
    public struct EnemySpawnWaveBlk
    {
        public float duration;
        public List<EnemySpawnGroupBlk> groups;
    }

    [System.Serializable]
    public struct EnemySpawnGroupBlk
    {
        public SpawningRange range;
        public EnemySpawningBlock spawningBlock;
        public SpawnLogic spawnLogic;
    }

    [System.Serializable]
    public struct EnemySpawningBlock
    {
        public int enemyID;
        public int count;
    }

    [System.Serializable]
    public struct SpawningRange
    {
        public float start;
        public float end;
    }
}