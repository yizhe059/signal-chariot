using InGame.Effects;
using InGame.Effects.EffectElement;

namespace Editors.Effects
{
    public class ChangeDefenseEffectEdit: EffectEdit
    {
        public int delta = 0;
        
        public override Effect CreateEffect()
        {
            return ChangeDefenseEffect.CreateEffect(delta);
        }
    }
}