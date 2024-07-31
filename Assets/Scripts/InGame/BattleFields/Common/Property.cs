using UnityEngine;
using UnityEngine.Events;

namespace InGame.BattleFields.Common
{
    public enum LimitedPropertyType
    {
        Health,
    }

    public enum UnlimitedPropertyType
    {
        Mod,
        Armor,
        Speed,
        BulletCount,
        Attack,
        Multiplier,
        Range,
        Interval,
    }

    public class UnlimitedProperty
    {
        public UnityEvent<float> onValueChanged = new();
        public UnlimitedPropertyType type { get; private set; }

        private float m_value;
        public float value
        {
            get => m_value;
            private set
            {
                if(value <= 0) m_value = 0;
                else m_value = value;
                onValueChanged.Invoke(m_value);
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

        public UnlimitedProperty(UnlimitedPropertyType type)
        {
            this.value = 0;
            this.type = type;
        }

        public UnlimitedProperty(float initial, UnlimitedPropertyType type)
        {
            this.value = initial;
            this.type = type;
        }
    }

    public class LimitedProperty
    {
        public UnityEvent<float, float> onValueChanged = new();
        public LimitedPropertyType type { get; private set; }

        private float m_current;
        public float current
        {
            get => m_current;
            private set
            {
                m_current = Mathf.Clamp(value, 0, m_max);
                onValueChanged.Invoke(m_current, m_max);
            }
        }

        private float m_max;
        public float max
        {
            get => m_max;
            private set
            {
                m_max = Mathf.Min(0, value);
                onValueChanged.Invoke(m_current, m_max);
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

        public LimitedProperty(float max, LimitedPropertyType type)
        {
            this.m_max = max;
            this.m_current = max;
            this.type = type;
        }

        public LimitedProperty(float max, float initial, LimitedPropertyType type)
        {
            this.m_max = max;
            this.current = initial;
            this.type = type;
        }
    }
}