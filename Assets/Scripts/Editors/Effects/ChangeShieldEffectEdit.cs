using InGame.Effects;

namespace Editors.Effects
{
    public class ChangeShieldEffectEdit: EffectEdit
    {
        public int delta = 0;
        
        public override Effect CreateEffect()
        {
            return ChangeShieldEffect.CreateEffect(delta);
        }
    }
}