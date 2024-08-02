using System.Collections.Generic;
using SetUps;
using UnityEngine;

namespace Editors.Enemies
{
    public class EnemySpawnLevelEdit: MonoBehaviour
    {
        public EnemySpawnLevelSetUp CreteLevelSetUp()
        {
            var waveEdits = transform.GetComponentsInChildren<EnemySpawnWaveEdit>();
            var waveBlks = new List<EnemySpawnWaveBlk>();
            foreach (var waveEdit in waveEdits)
            {
                waveBlks.Add(waveEdit.CreateEnemySpawnWaveBlk());
            }

            return new EnemySpawnLevelSetUp { waves = waveBlks };
        }
    }
}