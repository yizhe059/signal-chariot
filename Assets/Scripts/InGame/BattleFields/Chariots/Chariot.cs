using SetUps;

namespace Chariots
{
    public class Chariot
    {
        private LimitedProperty m_health;
        private UnlimitedProperty m_armor;

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
    }
}
