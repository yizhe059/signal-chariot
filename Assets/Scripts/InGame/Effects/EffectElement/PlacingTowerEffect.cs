using InGame.BattleFields.Androids;
using InGame.Cores;
using SetUps;

namespace InGame.Effects.EffectElement
{
    public class PlacingTowerEffect: Effect
    {
        public TowerSetUp setUp;
        private Tower m_tower;
        
        
        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            m_tower = GameManager.Instance.GetAndroid().GetTowerManager().AddTower(setUp, m_module);
        }

        public override void OnUnTrigger(EffectBlackBoard blackBoard)
        {
            GameManager.Instance.GetAndroid().GetTowerManager().RemoveTower(m_tower);
        }

        public override Effect CreateCopy()
        {
            return new PlacingTowerEffect
            {
                setUp = setUp
            };
        }

        public static PlacingTowerEffect CreateEffect(TowerSetUp newSetUp)
        {
            return new PlacingTowerEffect
            {
                setUp = newSetUp
            };
        }
    }
}