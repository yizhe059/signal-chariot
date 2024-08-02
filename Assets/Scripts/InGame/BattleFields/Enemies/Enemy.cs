using UnityEngine;

using InGame.BattleFields.Common;
using InGame.Views;
using SetUps;

namespace InGame.BattleFields.Enemies
{
    public class Enemy
    {
        
        private LimitedProperty m_health;
        private UnlimitedProperty m_speed;
        private UnlimitedProperty m_damage;
        private UnlimitedProperty m_range;
        private UnlimitedProperty m_attackInterval;

        public LimitedProperty health { get { return m_health; }}
        public UnlimitedProperty speed { get { return m_speed;}}
        public UnlimitedProperty damage { get { return m_damage;}}
        public UnlimitedProperty range { get { return m_range;}}
        
        public UnlimitedProperty attackInterval{ get { return m_attackInterval;}}
        public string name { get; private set; }
        
        public int typeID { get; private set; }

        private EnemyView m_viewPrefab;
        private EnemyView m_view = null;

        public EnemyView CreateView()
        {
            if (m_viewPrefab == null) return null;
            if (m_view != null) return m_view;

            m_view = GameObject.Instantiate(m_viewPrefab);
            m_view.Init(this);
            return m_view;
        }
        
        public static Enemy CreateEnemy(Enemy other)
        {
            return new Enemy
            {
                name = other.name,
                typeID = other.typeID,
                m_viewPrefab = other.m_viewPrefab,
                m_health = new LimitedProperty(other.health.max, LimitedPropertyType.Health),
                m_damage = new UnlimitedProperty(other.damage.value, UnlimitedPropertyType.Attack),
                m_range = new UnlimitedProperty(other.range.value, UnlimitedPropertyType.Range),
                m_attackInterval = new UnlimitedProperty(other.attackInterval.value, UnlimitedPropertyType.Interval),
                m_speed = new UnlimitedProperty(other.speed.value, UnlimitedPropertyType.Speed)
            };
        }
        
        public static Enemy CreateEnemy(EnemySetUp setUp, int typeID)
        {
            return new Enemy
            {
                name = setUp.name,
                typeID = typeID,
                m_viewPrefab = setUp.enemyPrefab,
                m_health = new LimitedProperty(setUp.maxHealth, LimitedPropertyType.Health),
                m_damage = new UnlimitedProperty(setUp.attack, UnlimitedPropertyType.Attack),
                m_range = new UnlimitedProperty(setUp.attackRadius, UnlimitedPropertyType.Range),
                m_attackInterval = new UnlimitedProperty(setUp.attackDuration, UnlimitedPropertyType.Interval),
                m_speed = new UnlimitedProperty(setUp.speed, UnlimitedPropertyType.Speed)
            };
        }
        
        
    }
}