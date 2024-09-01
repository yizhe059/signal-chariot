using InGame.BattleFields.Common;
using InGame.Effects;
using InGame.Effects.EffectElement;

namespace Editors.Effects
{
    public class ChangeLimitedPropertyEffectEdit: EffectEdit
    {
        public LimitedPropertyType propertyType;
        public int delta;
        public bool isCurrent = true;


        public override Effect CreateEffect()
        {
            return ChangeLimitedPropertyEffect.CreateEffect(propertyType, delta, isCurrent);
        }
    }
}