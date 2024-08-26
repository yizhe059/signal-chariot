using InGame.Effects;
using InGame.Effects.EffectElement;
using SetUps;

namespace Editors.Effects
{
    public class PlacingTowerEffectEdit: EffectEdit
    {
        public TowerSetUp setUp;
        public override Effect CreateEffect()
        {
            return PlacingTowerEffect.CreateEffect(new TowerSetUp(setUp));
        }
    }
}