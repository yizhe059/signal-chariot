using System;
using System.Collections.Generic;
using SetUps;
using UnityEngine;

namespace Editors.Enemies
{
    [System.Serializable]
    public class RewardBlk
    {
        public int moduleID;
        public string moduleName;
    }
    
    [System.Serializable]
    public class TemporaryWaveBlk
    {
        public int waveID;
                
        [Tooltip("波次胜利时，给予玩家的模块")]
        public List<RewardBlk> moduleRewards;
    }
    
    public class EnemySpawnLevelEdit: MonoBehaviour
    {
        public List<TemporaryWaveBlk> waves;

        public void OnValidate()
        {
            foreach (var wave in waves)
            {
                foreach (var reward in wave.moduleRewards)
                {
                    var id = reward.moduleID;

                    int count = EnemyRoot.Instance.setUp.moduleLibrary.Count;

                    if (id >= 0 && id < count)
                    {
                        reward.moduleName = EnemyRoot.Instance.setUp.moduleLibrary[id].name;
                    }
                    else
                    {
                        reward.moduleName = "Invalid ID";
                    }
                }
            }
            
        }

        public EnemyPlainLevelBlk CreteBlk()
        {
            
            var level = new EnemyPlainLevelBlk
            {
                waves = new List<EnemySpawnWaveBlk>()
            };
            foreach (var blk in waves)
            {
                var rewards = new List<int>();
                foreach (var rewardBlk in blk.moduleRewards)
                {
                    if (rewardBlk.moduleName == "Invalid ID")
                    {
                        Debug.LogError("Invalid moduleID!");
                        continue;
                    }
                    
                    rewards.Add(rewardBlk.moduleID);
                }
                
                level.waves.Add(new EnemySpawnWaveBlk
                {
                    waveID = blk.waveID,
                    moduleRewards = rewards
                });
            }

            return level;
        }
    }
}