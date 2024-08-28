using UnityEngine;

using Utils.Common;

namespace InGame.BattleEffects
{
    public class RangeOnceDamageEffect : Effect
    {
        private Vector3 m_center;
        private float m_radius;
        private int m_damage;

        public RangeOnceDamageEffect(Vector3 center, float radius, int damage) : base(1)
        {
            this.m_center = center;
            this.m_radius = radius;
            this.m_damage = damage;
        }

        public override void Trigger(GameObject go)
        {
            if (!IsActive) return;
            
            Collider[] colliders = Physics.OverlapSphere(m_center, m_radius);
            foreach (var collider in colliders)
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();
                damageable?.TakeDamage(m_damage);
            }
            m_count--;
        }
    }
}