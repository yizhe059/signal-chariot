using InGame.Effects;
using InGame.Effects.TriggerRequirements;
using UnityEngine;

namespace Editors.Effects.CustomEffectTrigger
{
    public abstract class CustomEffectTriggerEdit: MonoBehaviour
    {
        public abstract TriggerRequirement GetTrigger();
    }
}