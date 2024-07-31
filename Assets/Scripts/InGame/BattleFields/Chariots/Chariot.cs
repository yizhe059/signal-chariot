using System.Collections.Generic;

using UnityEngine;

using InGame.Views;
using InGame.Boards.Modules;
using InGame.BattleFields.Common;
using SetUps;
using Utils;
using UnityEngine.Events;

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
        private UnlimitedProperty m_mod;

        [Header("Tower")]
        private List<Tower> m_towers;
        
        public Chariot(ChariotSetUp setUp)
        {
            m_health = new LimitedProperty(
                setUp.maxHealth, 
                setUp.initialHealth, 
                LimitedPropertyType.Health
            );

            m_armor = new UnlimitedProperty(
                setUp.armor,
                UnlimitedPropertyType.Armor
            );

            m_speed = new UnlimitedProperty(
                setUp.speed,
                UnlimitedPropertyType.Speed
            );

            m_mod = new UnlimitedProperty(
                setUp.mod,
                UnlimitedPropertyType.Mod
            );

            m_towers = new();

            CreateView();
        }

        #region View
        public ChariotView chariotView { get { return m_chariotView;}}

        private void CreateView()
        {
            GameObject chariotPref = Resources.Load<GameObject>(Constants.GO_CHARIOT_PATH);
            GameObject chariotGO = GameObject.Instantiate(chariotPref);
            chariotGO.transform.position = new(0, 0, Constants.CHARIOT_DEPTH);

            m_chariotView = chariotGO.GetComponent<ChariotView>();
            m_chariotView.Init(this);
        }
        #endregion

        #region Properties
        public LimitedProperty health { get { return m_health;}}
        public UnlimitedProperty armor { get { return m_armor;}}
        public UnlimitedProperty speed { get { return m_speed;}}
        public UnlimitedProperty mod { get { return m_mod;}}

        public void RegisterLimitedPropertyEvent(LimitedPropertyType type, 
                                                UnityAction<float, float> call)
        {
            LimitedProperty property = GetLimitedProperty(type);
            Debug.Assert(property != null, "LimitedProperty Type does not exist");
            property.onValueChanged.AddListener(call);
            property.onValueChanged.Invoke(property.current, property.max);
        }

        public void UnregisterLimitedPropertyEvent(LimitedPropertyType type, 
                                                UnityAction<float, float> call)
        {
            LimitedProperty property = GetLimitedProperty(type);
            Debug.Assert(property != null, "LimitedProperty Type does not exist");
            property.onValueChanged.RemoveListener(call);
        }

        public void RegisterUnlimitedPropertyEvent(UnlimitedPropertyType type, 
                                                UnityAction<float> call)
        {
            UnlimitedProperty property = GetUnlimitedProperty(type);
            Debug.Assert(property != null, "UnlimitedProperty Type does not exist");
            property.onValueChanged.AddListener(call);
            property.onValueChanged.Invoke(property.value);
        }

        public void UnregisterUnlimitedPropertyEvent(UnlimitedPropertyType type, 
                                                    UnityAction<float> call)
        {
            UnlimitedProperty property = GetUnlimitedProperty(type);
            Debug.Assert(property != null, "UnlimitedProperty Type does not exist");
            property.onValueChanged.RemoveListener(call);
        }

        private LimitedProperty GetLimitedProperty(LimitedPropertyType type)
        {
            return type switch
            {
                LimitedPropertyType.Health => m_health,
                _ => null
            };
        }

        private UnlimitedProperty GetUnlimitedProperty(UnlimitedPropertyType type)
        {
            return type switch
            {
                UnlimitedPropertyType.Armor => m_armor,
                UnlimitedPropertyType.Mod => m_mod,
                UnlimitedPropertyType.Speed => m_speed,
                _ => null
            };
        }
        #endregion

        #region Towers
        public List<Tower> GetTowers() => m_towers;
        
        public void CopyTowers(List<Tower> towers)
        {
            m_towers = towers;
        }

        public Tower AddTower(TowerSetUp towerSetUp, Module module)
        {
            Tower tower = new(towerSetUp, module);
            m_towers.Add(tower);
            return tower;
        }

        public void RemoveTower(Tower tower)
        {
            m_towers.Remove(tower);
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
