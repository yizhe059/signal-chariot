using InGame.Effects;
using InGame.Effects.EffectElement;

namespace Editors.Effects
{
    public class SignalDirectionChangeEffectEdit: EffectEdit
    {

        public override Effect CreateEffect()
        {
            return SignalDirectionChangeEffect.CreateEffect();
        }
    }
}