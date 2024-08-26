using InGame.Effects.TriggerRequirements;
using UnityEngine;

namespace Editors.Effects.CustomEffectTrigger
{
    public class TimeIntervalTriggerEdit: CustomEffectTriggerEdit
    {
        [Min(0.001f)]
        public float timeInterval = 0.1f;

        public bool canEffectInTest = false;
        public override TriggerRequirement GetTrigger()
        {
            return IntervalTimeTriggerRequirement.CreateRequirement(timeInterval, canEffectInTest);
        }
    }
}