using UnityEngine;
using Utils.Common;

namespace InGame.BattleEffects
{
    public class SingleOnceDamageEffect : Effect
    {
        protected int m_damage;

        public SingleOnceDamageEffect(int damage) : base(1)
        {
            this.m_damage = damage;
        }

        public override void Trigger(GameObject go)
        {
            Debug.Log("triggerred");
            if(!IsActive) return;
            Debug.Log("active");
            IDamageable damageable = go.GetComponent<IDamageable>();
            damageable?.TakeDamage(m_damage);
        }
    }
}