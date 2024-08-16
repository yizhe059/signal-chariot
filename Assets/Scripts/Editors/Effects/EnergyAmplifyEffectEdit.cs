using InGame.Effects;
using InGame.Effects.EffectElement;
using UnityEngine;

namespace Editors.Effects
{
    public class EnergyAmplifyEffectEdit: EffectEdit
    {
        [Min(0)]
        public int amplifiedAmount;
        public override Effect CreateEffect()
        {
            return EnergyAmplifyEffect.CreateEffect(amplifiedAmount);
        }
    }
}