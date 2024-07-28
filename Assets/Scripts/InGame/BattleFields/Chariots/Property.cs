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
    }

    public class UnlimitedProperty
    {
        private float m_current;
        public UnityEvent<PropertyType, float> onValueChanged = new();
        public PropertyType type { get; private set; }

        public float current
        {
            get => m_current;
            set
            {
                onValueChanged.Invoke(type, m_current);
            }
        }

        public UnlimitedProperty(PropertyType type)
        {
            this.m_current = 0;
            this.type = type;
        }

        public UnlimitedProperty(float initial, PropertyType type)
        {
            this.m_current = initial;
            this.type = type;
        }
    }

    public class LimitedProperty
    {
        private float m_current;
        private float m_max;

        public UnityEvent<PropertyType, float, float> onValueChanged = new();
        public PropertyType type { get; private set; }

        public float current
        {
            get => m_current;
            set
            {
                m_current = Mathf.Clamp(value, 0, m_max);
                onValueChanged.Invoke(type, m_current, m_max);
            }
        }

        public float max
        {
            get => m_max;
            set
            {
                m_max = Mathf.Min(0, value);
                onValueChanged.Invoke(type, m_current, m_max);
            }
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