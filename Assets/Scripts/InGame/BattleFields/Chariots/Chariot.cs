using System.Collections.Generic;

using UnityEngine;

using InGame.Views;
using InGame.Boards.Modules;
using InGame.BattleFields.Common;
using SetUps;

namespace InGame.BattleFields.Chariots
{
    public class Chariot
    {
        [Header("View")]
        private ChariotView m_chariotView;

        [Header("Properties")]       
        private LimitedProperty m_health;
        private UnlimitedProperty m_armor;
        private UnlimitedProperty m_speed;

        [Header("Tower")]
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

            CreateView();
        }

        #region View
        public ChariotView chariotView { get { return m_chariotView;}}

        private void CreateView()
        {
            GameObject chariotPref = Resources.Load<GameObject>("Prefabs/BattleField/ChariotView");
            GameObject chariotGO = GameObject.Instantiate(chariotPref);
            m_chariotView = chariotGO.GetComponent<ChariotView>();
            m_chariotView.Init(this);
        }
        #endregion

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
        }

        public void RemoveTower(Tower tower)
        {
            m_towers.Remove(tower);
            // TODO: remove towerView from chariotView
        }

        public void TowerEffect(Module module)
        {
            foreach(Tower tower in m_towers)
            {
                if(module == tower.module)
                    tower.Effect();
            }
        }
        #endregion
    }
}
