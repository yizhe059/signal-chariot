

namespace InGame.BattleEffects
{
    public class CountEffect : Effect
    {
        private int m_count;
        public bool IsActive => m_count > 0;

        public override void Trigger()
        {
            if(!IsActive) return;
            m_count--;
        }
    }
}