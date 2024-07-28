using InGame.Effects;

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