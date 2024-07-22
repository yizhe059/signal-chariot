using System.Collections.Generic;
using World.Modules;

namespace World.SetUps
{
    [System.Serializable]
    public class ModuleSetUp
    {
        public string name ="";
        public List<ModulePosition> otherPositions;
    }
}