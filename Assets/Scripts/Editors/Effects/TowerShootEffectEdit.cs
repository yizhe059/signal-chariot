using InGame.Effects;

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