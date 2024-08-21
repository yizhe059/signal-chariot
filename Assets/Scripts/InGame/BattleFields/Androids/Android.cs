using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

using InGame.Views;
using InGame.Cores;
using InGame.InGameStates;
using InGame.BattleFields.Common;
using SetUps;
using Utils;
using Utils.Common;

namespace InGame.BattleFields.Androids
{
    public class Android : IPropertyRegisterable
    {
        [Header("View")]
        private AndroidView m_androidView;

        [Header("Properties")]       
        private LimitedProperty m_health;
        private UnlimitedProperty m_defence;
        private UnlimitedProperty m_speed;
        private UnlimitedProperty m_mod;
        private UnlimitedProperty m_crystal;

        [Header("Tower")]
        private TowerManager m_towerManager;
        
        public Android(AndroidSetUp setUp)
        {
            m_health = new LimitedProperty(
                setUp.maxHealth, 
                setUp.initialHealth, 
                LimitedPropertyType.Health
            );

            m_defence = new UnlimitedProperty(
                setUp.defence,
                UnlimitedPropertyType.Defence
            );

            m_speed = new UnlimitedProperty(
                setUp.speed,
                UnlimitedPropertyType.Speed
            );

            m_mod = new UnlimitedProperty(
                setUp.mod,
                UnlimitedPropertyType.Mod
            );

            m_crystal = new UnlimitedProperty(
                setUp.crystal, 
                UnlimitedPropertyType.Crystal
            );

            m_towerManager = new();

            CreateView();
        }

        private void Die()
        {
            // m_towerManager.ClearTower();
            m_androidView.Die();
            GameManager.Instance.ChangeToBattleResultState(BattleResultType.Fail);
        }

        #region View
        public AndroidView androidView { get { return m_androidView;}}

        private void CreateView()
        {
            GameObject androidPref = Resources.Load<GameObject>(Constants.GO_ANDROID_PATH);
            GameObject androidGO = GameObject.Instantiate(androidPref);
            androidGO.transform.position = new(0, 0, Constants.ANDROID_DEPTH);

            m_androidView = androidGO.GetComponent<AndroidView>();
            m_androidView.Init(this);
        }
        #endregion

        #region Properties

        public float Get(LimitedPropertyType type, bool isCurrentValue)
        {
            LimitedProperty result = m_health;
            switch(type)
            {
                case LimitedPropertyType.Health:
                    result = m_health;
                    break;
            }
            
            if(isCurrentValue) return result.current;
            else return result.max;
        }

        public float Get(UnlimitedPropertyType type)
        {
            return type switch
            {
                UnlimitedPropertyType.Defence => m_defence.value,
                UnlimitedPropertyType.Mod => m_mod.value,
                UnlimitedPropertyType.Speed => m_speed.value,
                UnlimitedPropertyType.Crystal => m_crystal.value,
                _ => throw new NotImplementedException(),
            };
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
                UnlimitedPropertyType.Defence => m_defence,
                UnlimitedPropertyType.Mod => m_mod,
                UnlimitedPropertyType.Speed => m_speed,
                UnlimitedPropertyType.Crystal => m_crystal,
                _ => null
            };
        }

        public void Set(LimitedPropertyType type, float val, bool isCurrentValue)
        {
            LimitedProperty property = GetLimitedProperty(type);
            if(isCurrentValue) property.current = val;
            else property.max = val;
        }

        public void Set(UnlimitedPropertyType type, float val)
        {
            UnlimitedProperty property = GetUnlimitedProperty(type);
            property.value = val;
        }

        public void Increase(LimitedPropertyType type, float delta, bool isCurrentValue)
        {
            LimitedProperty property = GetLimitedProperty(type);
            property.current += delta;
            if(isCurrentValue) property.current += delta;
            else property.max += delta;
        }
        
        public void Increase(UnlimitedPropertyType type, float delta)
        {
            UnlimitedProperty property = GetUnlimitedProperty(type);
            property.value += delta;
        }

        public void Decrease(LimitedPropertyType type, float delta, bool isCurrentValue)
        {
            LimitedProperty property = GetLimitedProperty(type);
            if(isCurrentValue) property.current -= delta;
            else property.max -= delta;
        }

        public void Decrease(UnlimitedPropertyType type, float delta)
        {
            UnlimitedProperty property = GetUnlimitedProperty(type);
            property.value -= delta;
        }

        public void RegisterPropertyEvent(LimitedPropertyType type, 
                                        UnityAction<float, float> call)
        {
            LimitedProperty property = GetLimitedProperty(type);
            Debug.Assert(property != null, "LimitedProperty Type does not exist");
            property.onValueChanged.AddListener(call);
            property.onValueChanged.Invoke(property.current, property.max);
        }

        public void UnregisterPropertyEvent(LimitedPropertyType type, 
                                            UnityAction<float, float> call)
        {
            LimitedProperty property = GetLimitedProperty(type);
            Debug.Assert(property != null, "LimitedProperty Type does not exist");
            property.onValueChanged.RemoveListener(call);
        }

        public void RegisterPropertyEvent(UnlimitedPropertyType type, 
                                        UnityAction<float> call)
        {
            UnlimitedProperty property = GetUnlimitedProperty(type);
            Debug.Assert(property != null, "UnlimitedProperty Type does not exist");
            property.onValueChanged.AddListener(call);
            property.onValueChanged.Invoke(property.value);
        }

        public void UnregisterPropertyEvent(UnlimitedPropertyType type, 
                                            UnityAction<float> call)
        {
            UnlimitedProperty property = GetUnlimitedProperty(type);
            Debug.Assert(property != null, "UnlimitedProperty Type does not exist");
            property.onValueChanged.RemoveListener(call);
        }
        #endregion

        #region Tower

        public TowerManager GetTowerManager() => m_towerManager;

        #endregion

        #region Logic

        public void TakeDamage(float dmg)
        {
            dmg -= m_defence.value;
            dmg = Mathf.Max(dmg, 0);
            m_health.current -= dmg;
            
            if(m_health.current <= 0) Die();
        }

        #endregion

        public Vector2 GetPosition()
        {
            return m_androidView.GetPosition();
        }
    }
}
