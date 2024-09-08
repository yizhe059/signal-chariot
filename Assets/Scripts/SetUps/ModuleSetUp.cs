using System.Collections.Generic;

using InGame.Boards.Modules;
using InGame.Boards.Modules.ModuleBuffs;
using InGame.Boards.Signals;
using InGame.Effects;
using InGame.Effects.PlacingEffectRequirements;
using InGame.Effects.TriggerRequirements;
using InGame.Views;

using UnityEngine;

namespace SetUps
{
    [System.Serializable]
    public class ModuleSetUp
    {
        public string name ="";
        public string desc = "";
        public string tag = "";
        public string function = "";
        public Category category = Category.Default;
        public List<ModulePosition> otherPositions;
        public ModuleView prefab;
        public ModuleBuffType buffMask;
        
        #region SignalEffects

        [SerializeReference]
        public List<Effect> signalEffects;
        public int maxUses;
        public SignalEffects.EnergyConsumptionMethod consumptionMethod;
        public int energyConsumption;
        public int maxStoredPerTrigger;
        public float coolDown;
        public SignalType signalMask;
        #endregion
        
        #region PlacingEffects
        
        [SerializeReference]
        public List<Effect> placingEffects;
        
        [SerializeReference]
        public List<PlacingEffectRequirement> requirements;

        #endregion
        
        #region CustomEffects

        public bool hasCustomEffect;
        [SerializeReference]
        public TriggerRequirement triggerRequirement;
        [SerializeReference]
        public List<Effect> customEffects;

        #endregion
    }
}