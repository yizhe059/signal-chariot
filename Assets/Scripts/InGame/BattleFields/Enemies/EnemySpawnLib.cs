

using System.Collections.Generic;
using SetUps;
using UnityEngine;

namespace InGame.BattleFields.Enemies
{
    public class EnemySpawnLib
    {
        private readonly List<EnemyPlainLevelBlk> m_levelsSetUp;
        private readonly List<EnemyPlainWaveBlk> m_wavesSetUp;
        private readonly List<EnemyPlainGroupSpawnBlk> m_groupsSetUp;
        
        public EnemySpawnLib(EnemySpawnSetUp setUp)
        {
            m_levelsSetUp = setUp.enemySpawnLevels;
            m_wavesSetUp = setUp.enemySpawnWaves;
            m_groupsSetUp = setUp.enemySpawnGroups;
        }

        public bool GetLevelSetUp(int levelID, out EnemyPlainLevelBlk blk)
        {
            if (levelID < 0 || levelID >= m_levelsSetUp.Count)
            {
                blk = default;
                return false;
            }

            blk = m_levelsSetUp[levelID].CreateCopy();
            return true;
        }

        public bool GetWaveSetUp(int waveID, out EnemyPlainWaveBlk blk)
        {
            if (waveID < 0 || waveID >= m_wavesSetUp.Count)
            {
                blk = default;
                return false;
            }

            blk = m_wavesSetUp[waveID].CreateCopy();
            return true;
        }

        public bool GetGroupSetUp(int groupID, out EnemyPlainGroupSpawnBlk blk)
        {
            if (groupID < 0 || groupID >= m_groupsSetUp.Count)
            {
                blk = default;
                return false;
            }

            blk = m_groupsSetUp[groupID].CreateCopy();
            return true;
        }

        // private EnemySpawnWaveBlk GetWaveBlk(int level, int wave)
        // {
        //     if (level < 0 || level >= m_levels.Count)
        //     {
        //         Debug.LogError("Level out of index");
        //         return default;
        //     }
        //
        //     var waves = m_levels[level].waves;
        //     
        //     if (wave < 0 || wave >= waves.Count)
        //     {
        //         Debug.LogError("Wave out of index");
        //         return default;
        //     }
        //
        //     return waves[wave];
        //
        // }

        

        
    }
}