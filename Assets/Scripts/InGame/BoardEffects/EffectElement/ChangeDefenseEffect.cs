using InGame.BattleFields.Common;
using InGame.Cores;

namespace InGame.Effects.EffectElement
{
    public class ChangeDefenseEffect: Effect
    {
        public int delta;
        
        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            GameManager.Instance.GetAndroid().Increase(UnlimitedPropertyType.Defense, delta);
        }

        public override void OnUnTrigger(EffectBlackBoard blackBoard)
        {
            GameManager.Instance.GetAndroid().Decrease(UnlimitedPropertyType.Defense, delta);
        }

        public override Effect CreateCopy()
        {
            return new ChangeDefenseEffect { delta = this.delta };
            
        }

        public static ChangeDefenseEffect CreateEffect(int delta)
        {
            return new ChangeDefenseEffect
            {
                delta = delta
            };
        }
    }
}