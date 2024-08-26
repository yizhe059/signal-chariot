using InGame.Effects;
using InGame.Effects.EffectElement;

namespace Editors.Effects
{
    public class SignalGenerationFromUsedEnergyEffectEdit: EffectEdit
    {
        public override Effect CreateEffect()
        {
            return SignalGenerationFromUsedEnergyEffect.CreateEffect();
        }
    }
}