using Utils.Common;
using UnityEngine;

namespace InGame.BattleEffects
{
    public class SingleContinuousDamageEffect : Effect
    {
        private IDamageable m_target;
        private int m_damage;

        public SingleContinuousDamageEffect(int damage, IDamageable target, 
        float duration, float interval) : base(Mathf.FloorToInt(duration / interval))
        {
            this.m_damage = damage;
            this.m_target = target;
        }

        public override void Trigger()
        {
            if (!IsActive) return;
            m_target.TakeDamage(m_damage);
            m_count--;
            // TODO: Continuous m_damage logic here (e.g., wait for the interval to trigger again)
        }
    }
}