using UnityEngine;

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
        SpawnEffect
    }

    public enum TriggerType
    {
        Collision,
        Destruction
    }

    [System.Serializable]
    public abstract class Effect
    {
        protected int m_count;
        protected bool IsActive => m_count != 0; // count < 0 means infinity
        public Effect(int count)
        {
            this.m_count = count;
        }
        public abstract void Trigger(GameObject go);
    }
}