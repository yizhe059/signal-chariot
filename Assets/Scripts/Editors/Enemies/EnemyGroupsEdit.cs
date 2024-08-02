using System.Collections.Generic;
using SetUps;
using UnityEngine;
using Utils.Common;

namespace Editors.Enemies
{
    public class EnemyGroupsEdit: MonoSingleton<EnemyGroupsEdit>
    {
        public int groupsCount => transform.childCount;
        public List<EnemyPlainGroupSpawnBlk> GetBlks()
        {
            var groupEdits = transform.GetComponentsInChildren<EnemySpawnGroupEdit>();
            var groups = new List<EnemyPlainGroupSpawnBlk>();

            foreach (var groupEdit in groupEdits)
            {
                groups.Add(groupEdit.CreateSpawnGroupBlks());
            }

            return groups;
        }
    }
}