using InGame.Effects;
using InGame.Effects.EffectElement;

namespace Editors.Effects
{
    public class TowerShootEffectEdit: EffectEdit
    {
        public override Effect CreateEffect()
        {
            return TowerShootEffect.CreateEffect();
        }
    }
}