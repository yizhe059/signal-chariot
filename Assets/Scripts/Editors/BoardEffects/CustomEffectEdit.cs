using System.Collections.Generic;
using Editors.Effects.CustomEffectTrigger;
using InGame.Effects;
using InGame.Effects.TriggerRequirements;
using UnityEngine;

namespace Editors.Effects
{
    public class CustomEffectEdit: MonoBehaviour
    {
        private Transform m_triggerTransform;
        private Transform m_effectsTransform;
        
        public bool CreateCustomEffect(out TriggerRequirement requirement, out List<Effect> customEffects)
        {
            Transform triggerTransform = transform.Find("Trigger");
            Transform effectsTransform = transform.Find("Effects");

            var triggers = triggerTransform.GetComponents<CustomEffectTriggerEdit>();
            if (triggers.Length == 0)
            {
                requirement = null;
                customEffects = null;
                return false;
            }
            
            if (triggers.Length > 1)
            {
                Debug.Log("有多个触发条件，只用第一个条件");
            }

            var triggerEdit = triggers[0];

            requirement = triggerEdit.GetTrigger();

            var effectEdits = effectsTransform.GetComponents<EffectEdit>();

            var effects = new List<Effect>();

            foreach (var effectEdit in effectEdits)
            {
                effects.Add(effectEdit.CreateEffect());
            }

            customEffects = effects;
            return true;
        }
    }
}