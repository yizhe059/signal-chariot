using System.Collections.Generic;
using UnityEngine;
using World.Effects;

namespace Editors.Effects
{
    public class PlacingEffectEdit: MonoBehaviour
    {
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