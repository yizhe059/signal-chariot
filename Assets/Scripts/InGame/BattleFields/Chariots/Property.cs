using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace InGame.BattleFields.Chariots
{
    public enum PropertyType
    {
        Health, 
        Armor,
        Speed,
        BulletCount,
        Attack,
    }

    public class UnlimitedProperty
    {
        public UnityEvent<PropertyType, float> onValueChanged = new();
        public PropertyType type { get; private set; }

        private float m_value;
        public float value
        {
            get => m_value;
            private set
            {
                if(value <= 0) m_value = 0;
                else m_value = value;
                onValueChanged.Invoke(type, m_value);
            }
        }

        public void Set(float val)
        {
            value = val;
        }

        public void Increase(float delta)
        {
            value += delta;
        }

        public void Decrease(float delta)
        {
            value -= delta;
        }

        public UnlimitedProperty(PropertyType type)
        {
            this.value = 0;
            this.type = type;
        }

        public UnlimitedProperty(float initial, PropertyType type)
        {
            this.value = initial;
            this.type = type;
        }
    }

    public class LimitedProperty
    {
        public UnityEvent<PropertyType, float, float> onValueChanged = new();
        public PropertyType type { get; private set; }

        private float m_current;
        public float current
        {
            get => m_current;
            private set
            {
                m_current = Mathf.Clamp(value, 0, m_max);
                onValueChanged.Invoke(type, m_current, m_max);
            }
        }

        private float m_max;
        public float max
        {
            get => m_max;
            private set
            {
                m_max = Mathf.Min(0, value);
                onValueChanged.Invoke(type, m_current, m_max);
            }
        }

        public void SetCurrent(float val)
        {
            current = val;
        }

        public void IncreaseCurrent(float delta)
        {
            current += delta;
        }
        
        public void DecreaseCurrent(float delta)
        {
            current -= delta;
        }

        public void SetMax(float val)
        {
            max = val;
        }

        public void IncreaseMax(float delta)
        {
            max += delta;
        }

        public void DecreaseMax(float delta)
        {
            max -= delta;
        }

        public LimitedProperty(float max, PropertyType type)
        {
            this.m_max = max;
            this.m_current = max;
            this.type = type;
        }

        public LimitedProperty(float max, float initial, PropertyType type)
        {
            this.m_max = max;
            this.current = initial;
            this.type = type;
        }
    }
}