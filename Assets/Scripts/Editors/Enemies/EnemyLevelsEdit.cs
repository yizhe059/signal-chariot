using System.Collections.Generic;
using SetUps;
using Utils.Common;

#if UNITY_EDITOR
namespace Editors.Enemies
{
    public class EnemyLevelsEdit: MonoSingleton<EnemyLevelsEdit>
    {
        public List<EnemyPlainLevelBlk> GetBlks()
        {
            var levelEdits = transform.GetComponentsInChildren<EnemySpawnLevelEdit>();
            var levels = new List<EnemyPlainLevelBlk>();

            foreach (var levelEdit in levelEdits)
            {
                levels.Add(levelEdit.CreteBlk());
            }

            return levels;
        }
    }
}
#endif