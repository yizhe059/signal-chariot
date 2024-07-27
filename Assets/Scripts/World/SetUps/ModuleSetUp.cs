using System.Collections.Generic;
using UnityEngine;
using World.Effects;
using World.Modules;
using World.Views;

namespace World.SetUps
{
    [System.Serializable]
    public class ModuleSetUp
    {
        public string name ="";
        public string desc = "";
        public List<ModulePosition> otherPositions;
        public ModuleView prefab;
        
        #region SignalEffects

        [SerializeReference]
        public List<Effect> signalEffects;
        public int maxUses;
        public int energyConsumption;
        public float coolDown;
        #endregion
        
        #region PlacingEffects
        
        [SerializeReference]
        public List<Effect> placingEffects;

        #endregion
    }
}