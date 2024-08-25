using UnityEngine;
using Utils.Common;

namespace InGame.BattleEffects
{
    public class RangeContinuousDamageEffect : Effect
    {
        private Vector3 m_center;
        private float m_radius;
        private int m_damage;

        public RangeContinuousDamageEffect(Vector3 center, float radius, int damage, 
        float duration, float interval) : base(Mathf.FloorToInt(duration / interval))
        {
            this.m_center = center;
            this.m_radius = radius;
            this.m_damage = damage;
        }

        public override void Trigger()
        {
            if (!IsActive) return;
            
            Collider[] colliders = Physics.OverlapSphere(m_center, m_radius);
            foreach (var collider in colliders)
            {
                IDamageable m_damageable = collider.GetComponent<IDamageable>();
                m_damageable?.TakeDamage(m_damage);
            }
            m_count--;
            // TODO: Continuous m_damage logic here (e.g., wait for the interval to trigger again)
        }
    }
}