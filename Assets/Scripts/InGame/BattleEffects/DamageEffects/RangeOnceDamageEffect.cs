using UnityEngine;

using Utils.Common;

namespace InGame.BattleEffects
{
    public class RangeOnceDamageEffect : Effect
    {
        private float m_radius;
        private int m_damage;

        public RangeOnceDamageEffect(float radius, int damage) : base(1)
        {
            this.m_radius = radius;
            this.m_damage = damage;
        }

        public override void Trigger(GameObject go)
        {
            if (!IsActive) return;
            Vector3 center = go.transform.position;
            Collider[] colliders = Physics.OverlapSphere(center, m_radius);
            foreach (var collider in colliders)
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();
                damageable?.TakeDamage(m_damage);
            }
            m_count--;
        }
    }
}