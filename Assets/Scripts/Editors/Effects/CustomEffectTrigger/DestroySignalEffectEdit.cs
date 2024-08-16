using InGame.Effects;
using InGame.Effects.EffectElement;

namespace Editors.Effects.CustomEffectTrigger
{
    public class DestroySignalEffectEdit: EffectEdit
    {
        
        public override Effect CreateEffect()
        {
            return DestroySignalEffect.CreateEffect();
        }
    }
}