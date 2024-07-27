using System.Collections.Generic;
using InGame.Effects;
using UnityEngine;

namespace Editors.Effects
{
    public class SignalEffectEdit: MonoBehaviour
    {
        public int maxUses;
        public int energyConsumption;
        public float coolDown;
        
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