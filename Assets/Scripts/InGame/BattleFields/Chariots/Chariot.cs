using System.Collections.Generic;
using InGame.Views;
using SetUps;

namespace InGame.BattleFields.Chariots
{
    public class Chariot
    {
        private ChariotView m_chariotView;
        
        private LimitedProperty m_health;
        private UnlimitedProperty m_armor;
        private UnlimitedProperty m_speed;
        private List<Tower> m_towers;
        
        public Chariot(ChariotSetUp setUp)
        {
            m_health = new LimitedProperty(
                setUp.maxHealth, 
                setUp.initialHealth, 
                PropertyType.Health
            );

            m_armor = new UnlimitedProperty(
                setUp.armor,
                PropertyType.Armor
            );

            m_speed = new UnlimitedProperty(
                setUp.speed,
                PropertyType.Speed
            );
        }

        #region Properties
        public LimitedProperty health { get { return m_health;}}
        public UnlimitedProperty armor { get { return m_armor;}}
        public UnlimitedProperty speed { get { return m_speed;}}
        #endregion

        #region Towers
        public List<Tower> GetTowers() => m_towers;
        
        public void CopyTowers(List<Tower> towers)
        {
            m_towers = towers;
        }

        public void AddTower(Tower tower)
        {
            m_towers.Add(tower);
            // TODO: add towerView to chariotView
        }

        public void RemoveTower(Tower tower)
        {
            m_towers.Remove(tower);
            // TODO: remove towerView from chariotView
        }

        public void TowerEffect()
        {

        }
        #endregion
    }
}
