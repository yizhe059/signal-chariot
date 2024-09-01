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
        [SerializeField]
        protected int m_count;
        protected bool IsActive => m_count != 0; // count < 0 means infinity
        public Effect(int count)
        {
            this.m_count = count;
        }
        
        protected Effect(){}
        
        public abstract void Trigger(GameObject go);

        public Effect CreateCopy()
        {
            var newEffect = OnCreateCopy();
            newEffect.m_count = m_count;
            return newEffect;
        }
        
        protected abstract Effect OnCreateCopy();
    }
}