using InGame.BattleFields.Common;
using InGame.Cores;

namespace InGame.Effects.EffectElement
{
    public class ChangeLimitedPropertyEffect: Effect
    {
        public LimitedPropertyType propertyType;
        public int delta;
        public bool isCurrent;
        
        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            GameManager.Instance.GetAndroid().Increase(propertyType, delta, isCurrent);
        }

        public override void OnUnTrigger(EffectBlackBoard blackBoard)
        {
            GameManager.Instance.GetAndroid().Decrease(propertyType, delta, isCurrent);
        }

        public override Effect CreateCopy()
        {
            return new ChangeLimitedPropertyEffect
            {
                propertyType = this.propertyType,
                delta = this.delta,
                isCurrent = this.isCurrent
            };
            
        }

        public static ChangeLimitedPropertyEffect CreateEffect(LimitedPropertyType propertyType, int delta, bool isCurrent)
        {
            return new ChangeLimitedPropertyEffect
            {
                propertyType = propertyType,
                delta = delta,
                isCurrent = isCurrent
            };
        }
    }
}