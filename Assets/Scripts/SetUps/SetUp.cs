using System.Collections.Generic;
using UnityEngine;

namespace SetUps
{
    [CreateAssetMenu(fileName = "Set Up", menuName = "Set Up", order = 0)]
    public class SetUp: ScriptableObject
    {
        public BoardSetUp boardSetUp;

        public List<ModuleSetUp> moduleLibrary;
    }
}