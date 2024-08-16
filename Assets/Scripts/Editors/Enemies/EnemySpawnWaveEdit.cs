using System.Collections.Generic;
using System.Data;
using SetUps;
using UnityEngine;
using UnityEngine.Serialization;

namespace Editors.Enemies
{

    
    public class EnemySpawnWaveEdit: MonoBehaviour
    {
        
        
        public EnemyPlainWaveBlk wave;
        
        
        public void OnValidate()
        {
            if (wave.groupsMustBeat == null || wave.groups == null) return;
            for (int i = 0; i < wave.groupsMustBeat.Count; i++)
            {
                if (wave.groups.Count == 0) wave.groupsMustBeat[i] = -1;
                else
                {
                    if (wave.groupsMustBeat[i] < 0) wave.groupsMustBeat[i] = 0;
                    else if (wave.groupsMustBeat[i] >= wave.groups.Count)
                        wave.groupsMustBeat[i] = wave.groups.Count - 1;
                }
            }

            
            

        }
        public EnemyPlainWaveBlk CreateBlk()
        {

            return wave.CreateCopy();
        }
    }
}