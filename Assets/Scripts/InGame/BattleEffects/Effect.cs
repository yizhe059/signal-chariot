
namespace InGame.BattleEffects
{
    public enum EffectType
    {
        None,
        SingleOnceDamageEffect,
        SingleContinuousDamageEffect,
        RangeOnceDamageEffect,
        RangeContinuousDamageEffect,
        BouncingEffect,
        PenetrationEffect,
        SplittingEffect,
        SpawnAndDestroyEffect
    }

    public abstract class Effect
    {
        protected int m_count;
        protected bool IsActive => m_count > 0;
        public Effect(int count)
        {
            this.m_count = count;
        }
        public abstract void Trigger();
    }
}