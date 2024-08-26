using InGame.Effects;
using InGame.Effects.EffectElement;
using UnityEngine;

namespace Editors.Effects
{
    public class SignalGenerationEffectEdit: EffectEdit
    {
        [Min(1)]
        public int strength;
        public override Effect CreateEffect()
        {
            return SignalGenerationEffect.CreateEffect(strength);
        }
    }
}