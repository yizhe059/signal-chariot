using System.Collections.Generic;
using UnityEngine;

namespace SetUps
{
    [CreateAssetMenu(fileName = "Set Up", menuName = "Set Up", order = 0)]
    public class SetUp: ScriptableObject
    {
        public ChariotSetUp chariotSetUp;
        public BoardSetUp boardSetUp;
        public BoardSetUp extraBoardSetUp;
        public List<ModuleSetUp> moduleLibrary;
        public List<EnemySetUp> enemyLibrary;
        public List<EnemySpawnLevelSetUp> enemySpawns;
    }
}