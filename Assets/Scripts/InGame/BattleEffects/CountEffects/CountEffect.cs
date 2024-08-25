
namespace InGame.BattleEffects
{
    public class CountEffect : Effect
    {
        public CountEffect(int count) : base(count){}

        public override void Trigger()
        {
            if(!IsActive) return;
            m_count--;
        }
    }
}