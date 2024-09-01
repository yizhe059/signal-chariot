using System.Collections.Generic;
using InGame.Boards.Signals;
using InGame.Effects;
using UnityEngine;

namespace Editors.Effects
{
    public class SignalEffectEdit: MonoBehaviour
    {
        public int maxUses;
        public SignalEffects.EnergyConsumptionMethod consumptionMethod = SignalEffects.EnergyConsumptionMethod.Fixed;
        [Min(0)]
        public int energyConsumption;
        [Min(0)]
        public int maxStoredPerTrigger;
        public float coolDown;
        public SignalType mask = SignalType.Normal;
        
        public List<Effect> CreateEffects()
        {
            var effectEdits = transform.GetComponents<EffectEdit>();
            
            var effects = new List<Effect>();

            foreach (var effectEdit in effectEdits)
            {
                effects.Add(effectEdit.CreateEffect());
            }

            return effects;
        }
    }
}