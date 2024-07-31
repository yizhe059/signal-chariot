using InGame.Effects;
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