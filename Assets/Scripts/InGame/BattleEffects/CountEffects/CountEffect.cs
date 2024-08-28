using UnityEngine;

namespace InGame.BattleEffects
{
    public class CountEffect : Effect
    {
        public CountEffect(int count) : base(count){}

        public override void Trigger(GameObject go)
        {
            if(!IsActive) return;
            m_count--;
        }
    }
}