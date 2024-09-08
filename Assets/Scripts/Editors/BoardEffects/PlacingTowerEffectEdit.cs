using InGame.Effects;
using InGame.Effects.EffectElement;
using SetUps;

namespace Editors.Effects
{
    public class PlacingEquipmentEffectEdit: EffectEdit
    {
        public EquipmentSetUp setUp;
        public override Effect CreateEffect()
        {
            return PlacingEquipmentEffect.CreateEffect(new EquipmentSetUp(setUp));
        }
    }
}