using Utils.Common;

namespace InGame.BattleEffects
{
    public class SingleOnceDamageEffect : Effect
    {
        protected IDamageable m_target;
        protected int m_damage;
        public SingleOnceDamageEffect(int damage, IDamageable target) : base(1)
        {
            this.m_damage = damage;
            this.m_target = target;
        }
        public override void Trigger()
        {
            if(!IsActive) return;
            m_target.TakeDamage(m_damage);
        }
    }
}