using UnityEngine;
using Utils.Common;

namespace InGame.BattleEffects
{
    public class RangeContinuousDamageEffect : Effect
    {
        [SerializeField]
        private float m_radius;
        [SerializeField]
        private int m_damage;

        public RangeContinuousDamageEffect(float radius, int damage, 
        float duration, float interval) : base(Mathf.FloorToInt(duration / interval))
        {
            this.m_radius = radius;
            this.m_damage = damage;
        }
        
        private RangeContinuousDamageEffect(){}
        
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
            // TODO: Continuous m_damage logic here (e.g., wait for the interval to trigger again)
        }

        protected override Effect OnCreateCopy()
        {
            return new RangeContinuousDamageEffect
            {
                m_radius = m_radius,
                m_damage = m_damage
            };
        }
    }
}