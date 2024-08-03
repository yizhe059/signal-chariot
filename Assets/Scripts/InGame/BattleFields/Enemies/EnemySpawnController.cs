using System.Collections.Generic;
using SetUps;
using UnityEngine;
using UnityEngine.Events;

namespace InGame.BattleFields.Enemies
{
    public enum SpawnLogic
    {
        Random
    }

    public class EnemySpawnController
    {
        private class EnemyBlk
        {
            public Enemy enemy;
        }
        private EnemySpawnLib m_spawnLib;
        private EnemyLib m_enemyLib;
        private int m_currentWaveIdx;
        private List<int> m_waves = new List<int>();
        private EnemyWaveSpawnController m_currentWaveController;
        private bool m_isOn = false;

        private readonly UnityEvent m_waveFinishCallBack = new();
        private readonly List<EnemyBlk> m_enemies = new List<EnemyBlk>();

        public EnemySpawnController(EnemySpawnLib spawnLib, EnemyLib enemyLib)
        {
            m_spawnLib = spawnLib;
            m_enemyLib = enemyLib;
            currentEnemies = new();
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

        public void Start()
        {
            m_isOn = true;
            m_currentWaveController?.Start();
        }

        public void Stop()
        {
            m_isOn = false;
        }

        public void RegisterWaveFinishCallBack(UnityAction act)
        {
            if (act == null) return;
            m_waveFinishCallBack.AddListener(act);
        }

        public void UnregisterWaveFinishCallBack(UnityAction act)
        {
            if (act == null) return;
            m_waveFinishCallBack.RemoveListener(act);
        }
        
        public void Update(float deltaTime)
        {
            if (!m_isOn) return;
            m_currentWaveController?.Update(deltaTime);
        }

        public void Clear()
        {
            m_currentWaveController?.Clear();
        }
        
        public void FinishWaveCallBack()
        {
            Debug.Log("Wave Finished");
            m_waveFinishCallBack.Invoke();
            
        }
        
        public Enemy GenerateEnemy(int enemyIdx)
        {

            var enemy = m_enemyLib.CreateEnemy(enemyIdx);
            var enemyBlk = new EnemyBlk
            {
                enemy = enemy
            };
            
            m_enemies.Add(enemyBlk);
            
            
            Debug.Log($"Generate Enemy with ID {enemyIdx}");
            return enemy;
        }

        public Enemy GetClosestEnemy(Vector3 position)
        {   
            Enemy closest = null;
            float distance = float.MaxValue;
            Vector2 pos = new Vector2(position.x, position.y);

            foreach(EnemyBlk enemyBlk in m_enemies)
            {
                float prevDistance = distance;
                Vector2 enemyPos = new Vector2(
                    enemyBlk.enemy.GetView().transform.position.x, 
                    enemyBlk.enemy.GetView().transform.position.y
                );

                distance = Mathf.Min(distance, Vector2.Distance(pos, enemyPos));
                if(distance < prevDistance) closest = enemy;
            }

            return closest;
        }
    }

    public class EnemyWaveSpawnController
    {   
        private class EnemyGroup
        {
            public EnemyGroupSpawnController controller;
            public SpawningRange range;
            public bool mustBeat;
            public bool isStarted { get; set; }
            

            public void SetMustBeat(bool val)
            {
                mustBeat = val;
            }

            public void SetIsStarted(bool val)
            {
                isStarted = val;
            }
        }
        private EnemySpawnLib m_spawnLib;
        private EnemySpawnController m_spawnController;
        

        private List<EnemyGroup> m_groups = new List<EnemyGroup>();
        private float m_duration;
        private float m_timer;
        private bool m_isOn;
        private int m_numGroupHasToBeat;
        
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
 
            foreach (var groupSetUp in setUp.groups)
            {
                
                var groupController = new EnemyGroupSpawnController(m_spawnLib, m_spawnController, this);
                m_groups.Add(
                    new EnemyGroup
                    {
                        controller = groupController,
                        range = groupSetUp.range,
                        mustBeat = false,
                        isStarted = false
                    }
                );
                groupController.Init(groupSetUp.spawnGroupID, m_groups.Count - 1);

            }
            
            Debug.Log(m_groups.Count);

            m_numGroupHasToBeat = setUp.groupsMustBeat.Count;
            foreach (var groupIdx in setUp.groupsMustBeat)
            {
                m_groups[groupIdx].SetMustBeat(true);
            }
        }

        public void Start()
        {
            m_timer = 0f;
            m_isOn = true;
        }
        
        public void Update(float deltaTime)
        {
            if (!m_isOn) return;
            m_timer += deltaTime;
            foreach (var enemyGroup in m_groups)
            {
                if (m_timer >= enemyGroup.range.start && !enemyGroup.isStarted)
                {
                    enemyGroup.controller?.Start();
                    enemyGroup.SetIsStarted(true);
                }

                enemyGroup.controller?.Update(deltaTime);
                
                if (m_timer >= enemyGroup.range.end)
                {
                    enemyGroup.controller?.Stop();
                }
            }

            if (m_timer >= m_duration && m_numGroupHasToBeat <= 0)
            {
                m_spawnController.FinishWaveCallBack();
                
            }
        }

        public void Clear()
        {
            foreach (var group in m_groups)
            {
                group.controller?.Clear();
            }
            m_groups.Clear();
        }
        
        public void GroupBeatenCallBack(int idx)
        {
            if (m_groups[idx].mustBeat)
            {
                m_numGroupHasToBeat--;
            }
        }
    }
    
    public class EnemyGroupSpawnController
    {
        private class EnemyBlock
        {
            public Enemy enemy { get; set; }
            public int enemyID { get; set; }
            public bool isDead { get; set; }
            public UnityAction callBack { get; set; }
            
        }
        private EnemySpawnLib m_spawnLib;
        private EnemySpawnController m_spawnController;
        private EnemyWaveSpawnController m_parent;
        private int m_ID;

        private List<EnemyBlock> m_enemies = new List<EnemyBlock>();
        private int m_numOfEnemies = 0;
        private List<int> m_enemyPool = new List<int>();
        private int m_eachSpawnCount;
        private float m_spawnInterval;
        private int m_minEnemies;
        private int m_maxEnemies;
        private SpawnLogic m_locationSpawnLogic;

        private float m_timer;
        private bool m_isOn;
        
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

            m_isOn = false;

        }

        public void Start()
        {
            m_timer = m_spawnInterval;
            m_isOn = true;
        }

        public void Stop()
        {
            m_isOn = false;
        }

        public void Clear()
        {
            foreach (var blk in m_enemies)
            {
                if (blk.enemy != null && !blk.isDead)
                {
                    blk.enemy.UnregisterDieCallBack(blk.callBack);
                    blk.enemy = null;
                    blk.callBack = null;
                }
            }
            
            m_enemies.Clear();
        }
        public void Update(float deltaTime)
        {
            if (!m_isOn) return;
            if (m_numOfEnemies >= m_maxEnemies)
            {
                m_timer = 0;
            }else if (m_numOfEnemies < m_minEnemies)
            {
                SpawnEnemy();
                m_timer = 0;
            }
            else
            {
                m_timer += deltaTime;
                while (m_timer >= m_spawnInterval)
                {
                    m_timer -= m_spawnInterval;
                    SpawnEnemy();
                }
            }
        }

        private void SpawnEnemy()
        {
            int spawnCount = Mathf.Min(m_maxEnemies - m_numOfEnemies, m_eachSpawnCount);
            
            for (int i = 0; i < spawnCount; i++)
            {
                int poolIdx = Random.Range(0, m_enemyPool.Count);
                
                var enemy = m_spawnController.GenerateEnemy(m_enemyPool[poolIdx]);

                int idx = m_enemies.Count;
                var enemyBlk = new EnemyBlock
                {
                    enemy = enemy,
                    enemyID = m_enemyPool[poolIdx],
                    isDead = false,
                    callBack = () => EnemyIsDead(idx)
                };
                
                enemy?.RegisterDieCallBack(enemyBlk.callBack);
                m_enemies.Add(enemyBlk);
                m_numOfEnemies++;
                Debug.Log($"Enemy Spawn. ID: {m_ID}, total: {m_enemies.Count}, {m_timer}");
            }
        }

        private void EnemyIsDead(int idx)
        {
            var blk = m_enemies[idx];
            blk.isDead = true;
            blk.enemy.UnregisterDieCallBack(blk.callBack);
            blk.enemy = null;
            blk.callBack = null;

            m_numOfEnemies--;
            if (m_isOn) return;
            foreach (var enemyBlock in m_enemies)
            {
                if (!enemyBlock.isDead) return;
            }
            
            m_parent?.GroupBeatenCallBack(m_ID);
        }
    }
}