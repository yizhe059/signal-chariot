using System.Collections.Generic;
using SetUps;
using UnityEngine;
using Utils.Common;

namespace Editors.Enemies
{
    public class EnemyWavesEdit: MonoSingleton<EnemyWavesEdit>
    {
        public int waveCount => transform.childCount;
        public List<EnemyPlainWaveBlk> GetBlks()
        {
            var groupEdits = transform.GetComponentsInChildren<EnemySpawnWaveEdit>();
            var waves = new List<EnemyPlainWaveBlk>();

            foreach (var groupEdit in groupEdits)
            {
                waves.Add(groupEdit.CreateBlk());
            }

            return waves;
        }
    }
}