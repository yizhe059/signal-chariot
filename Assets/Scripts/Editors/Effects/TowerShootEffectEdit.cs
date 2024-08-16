using InGame.Effects;
using TowerShootEffect = InGame.Effects.EffectElement.TowerShootEffect;

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