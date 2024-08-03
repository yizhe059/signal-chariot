using System.Collections.Generic;
using SetUps;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace InGame.BattleFields.Enemies
{
    public enum SpawnLogic
    {
        Random
    }
    public class EnemySpawnController
    {
        private EnemySpawnLib m_spawnLib;
        private EnemyLib m_enemyLib;
        private int m_currentWaveIdx;
        private List<int> m_waves = new List<int>();
        private EnemyWaveSpawnController m_currentWaveController;

        public EnemySpawnController(EnemySpawnLib spawnLib, EnemyLib enemyLib)
        {
            m_spawnLib = spawnLib;
            m_enemyLib = enemyLib;
        }

        public void Init(int levelIdx)
        {
            if (!m_spawnLib.GetLevelSetUp(levelIdx, out var setUp))
            {
                Debug.LogError($"Wave Idx out of Bound {levelIdx}");
                return;
            }
            
            m_waves = new List<int>();
            foreach (var blk in setUp.waves)
            {
                m_waves.Add(blk.waveID);
            }

            m_currentWaveIdx = -1;
            m_currentWaveController = null;
        }
        /// <summary>
        /// Go to the next wave
        /// </summary>
        /// <returns>return false to indicate it is last wave</returns>
        public bool GoNextWave()
        {
            m_currentWaveIdx++;
            if (m_currentWaveIdx >= m_waves.Count) return false;

            m_currentWaveController = new EnemyWaveSpawnController(m_spawnLib, this);
            m_currentWaveController.Init(m_waves[m_currentWaveIdx]);
            return true;
        }
    }

    public class EnemyWaveSpawnController
    {
        
        private struct EnemyGroup
        {
            public EnemyGroupSpawnController controller;
            public SpawningRange range;
            public bool mustBeat;

            public void SetMustBeat(bool val)
            {
                mustBeat = val;
            }
        }
        private EnemySpawnLib m_spawnLib;
        private EnemySpawnController m_spawnController;
        

        private List<EnemyGroup> m_groups = new List<EnemyGroup>();
        private float m_duration;
        
        public EnemyWaveSpawnController(EnemySpawnLib spawnLib, EnemySpawnController controller)
        {
            m_spawnLib = spawnLib;
            m_spawnController = controller;
        }
        
        public void Init(int waveID)
        {
            if (!m_spawnLib.GetWaveSetUp(waveID, out var setUp))
            {
                Debug.LogError($"Wave Idx out of Bound {waveID}");
                return;
            }
            
            m_duration = setUp.duration;
            int id = 0;
            foreach (var groupSetUp in setUp.groups)
            {
                var groupController = new EnemyGroupSpawnController(m_spawnLib, m_spawnController, this);
                m_groups.Add(
                    new EnemyGroup
                    {
                        controller = groupController,
                        range = groupSetUp.range,
                        mustBeat = false
                    }
                );
                groupController.Init(groupSetUp.spawnGroupID, id);
                
            }

            foreach (var groupIdx in setUp.groupsMustBeat)
            {
                m_groups[groupIdx].SetMustBeat(true);
            }
        }

    }
    
    public class EnemyGroupSpawnController
    {
        private struct EnemyBlock
        {
            public Enemy enemy { get; set; }
            public int enemyID { get; set; }
            public bool isDead { get; set; }
            
        }
        private EnemySpawnLib m_spawnLib;
        private EnemySpawnController m_spawnController;
        private EnemyWaveSpawnController m_parent;
        private int m_ID;

        private List<EnemyBlock> m_enemies = new List<EnemyBlock>();
        private List<int> m_enemyPool = new List<int>();
        private int m_eachSpawnCount;
        private float m_spawnInterval;
        private int m_minEnemies;
        private int m_maxEnemies;
        private SpawnLogic m_locationSpawnLogic;
        
        
        public EnemyGroupSpawnController(EnemySpawnLib spawnLib, EnemySpawnController spawnController, EnemyWaveSpawnController parent)
        {
            m_spawnLib = spawnLib;
            m_spawnController = spawnController;
            m_parent = parent;
        }

        public void Init(int groupID, int myID)
        {
            m_ID = myID;
            
            if (!m_spawnLib.GetGroupSetUp(groupID, out var setUp))
            {
                Debug.LogError($"Wave Idx out of Bound {groupID}");
                return;
            }

            m_eachSpawnCount = setUp.eachSpawnCount;
            m_spawnInterval = setUp.spawnInterval;
            m_minEnemies = setUp.minEnemies;
            m_maxEnemies = setUp.maxEnemies;
            m_locationSpawnLogic = setUp.locationSpawnLogic;

            foreach (var enemyBlk in setUp.enemiesPool)
            {
                m_enemyPool.Add(enemyBlk.enemyID);
            }

        }
    }
}