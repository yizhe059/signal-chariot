using System.Collections.Generic;
using SetUps;

namespace InGame.BattleFields.Chariots
{
    public class Chariot
    {
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

        #region Health
        public float GetHealth()
        {
            return m_health.current;
        }

        public void SetHealth(int val)
        {
            m_health.current = val;
        }

        public void IncreaseHealth(int delta)
        {
            m_health.current += delta;
        }

        public void DecreaseHealth(int delta)
        {
            m_health.current -= delta;
        }
        #endregion

        #region Armor
        public float GetArmor()
        {
            return m_armor.current;
        }

        public void SetArmor(int val)
        {
            m_armor.current = val;
        }

        public void IncreaseArmor(int delta)
        {
            m_armor.current += delta;
        }

        public void DecreaseArmor(int delta)
        {
            m_armor.current -= delta;
        }
        #endregion

        #region Speed
        public float GetSpeed()
        {
            return m_speed.current;
        }
        public void SetSpeed(int val)
        {
            m_speed.current = val;
        }
        public void IncreaseSpeed(int delta)
        {
            m_speed.current += delta;
        }
        public void DecreaseSpeed(int delta)
        {
            m_speed.current -= delta;
        }
        #endregion
    }
}
