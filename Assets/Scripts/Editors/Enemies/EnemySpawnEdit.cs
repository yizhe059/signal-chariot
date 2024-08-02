using System.Collections.Generic;
using SetUps;
using UnityEngine;

namespace Editors.Enemies
{
    public class EnemySpawnEdit: MonoBehaviour
    {
        public List<EnemySpawnLevelSetUp> CreateSetUp()
        {
            var levelEdits = transform.GetComponentsInChildren<EnemySpawnLevelEdit>();
            var levels = new List<EnemySpawnLevelSetUp>();

            foreach (var levelEdit in levelEdits)
            {
                levels.Add(levelEdit.CreteLevelSetUp());
            }

            return levels;
        }
    }
}