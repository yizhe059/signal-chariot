using InGame.BattleFields.Common;
using InGame.Cores;

namespace InGame.Effects
{
    public class ChangeShieldEffect: Effect
    {
        public int delta;
        
        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            GameManager.Instance.GetChariot().Increase(UnlimitedPropertyType.Defence, delta);
        }

        public override void OnUnTrigger(EffectBlackBoard blackBoard)
        {
            GameManager.Instance.GetChariot().Decrease(UnlimitedPropertyType.Defence, delta);
        }

        public override Effect CreateCopy()
        {
            return new ChangeShieldEffect { delta = this.delta };
            
        }

        public static ChangeShieldEffect CreateEffect(int delta)
        {
            return new ChangeShieldEffect
            {
                delta = delta
            };
        }
    }
}