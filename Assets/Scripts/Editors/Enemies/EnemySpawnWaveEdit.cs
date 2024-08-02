using System.Collections.Generic;
using SetUps;
using UnityEngine;

namespace Editors.Enemies
{
    public class EnemySpawnWaveEdit: MonoBehaviour
    {
        public float duration;

        public EnemySpawnWaveBlk CreateEnemySpawnWaveBlk()
        {
            bool hasError = false;
            float prevDuration = duration;
            var groupEdits = transform.GetComponentsInChildren<EnemySpawnGroupEdit>();
            var groups = new List<EnemySpawnGroupBlk>();
            foreach (var groupEdit in groupEdits)
            {
                groups.Add(groupEdit.CreateSpawnGroupBlk());
                if (groupEdit.end > duration)
                {
                    duration = groupEdit.end;
                    hasError = true;
                }
            }

            if (hasError)
            {
                Debug.LogError($"每组的结束时间必须大于一波的时长，从{prevDuration}修改为{duration}");
            }
            
            

            return new EnemySpawnWaveBlk
            {
                duration = duration,
                groups = groups
            };
        }
    }
}