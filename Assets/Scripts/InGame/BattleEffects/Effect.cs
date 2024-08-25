
namespace InGame.BattleEffects
{
    public abstract class Effect
    {
        protected int m_count;
        protected bool IsActive => m_count > 0;
        public Effect(int count)
        {
            this.m_count = count;
        }
        public abstract void Trigger();
    }
}