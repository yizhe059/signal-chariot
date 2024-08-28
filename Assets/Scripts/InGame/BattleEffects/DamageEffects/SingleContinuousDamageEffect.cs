using Utils.Common;
using UnityEngine;

namespace InGame.BattleEffects
{
    public class SingleContinuousDamageEffect : Effect
    {
        private int m_damage;

        public SingleContinuousDamageEffect(int damage, float duration, float interval) : 
        base(Mathf.FloorToInt(duration / interval))
        {
            this.m_damage = damage;
        }

        public override void Trigger(GameObject go)
        {
            if (!IsActive) return;
            IDamageable damageable = go.GetComponent<IDamageable>();
            damageable?.TakeDamage(m_damage);
            m_count--;
            // TODO: Continuous m_damage logic here (e.g., wait for the interval to trigger again)
        }
    }
}