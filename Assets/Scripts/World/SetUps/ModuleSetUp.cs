using System.Collections.Generic;
using World.Modules;
using World.Views;

namespace World.SetUps
{
    [System.Serializable]
    public class ModuleSetUp
    {
        public string name ="";
        public List<ModulePosition> otherPositions;
        public ModuleView prefab;
    }
}