using UnityEngine;

using InGame.Views;
using SetUps;

namespace InGame.BattleFields.Chariots
{
    public class Bullet
    {
        private BulletView m_bulletView;
        
        private Vector3 m_target;
        private UnlimitedProperty m_speed;
        private UnlimitedProperty m_attack;

        public Vector3 target { get { return m_target; } }
        public UnlimitedProperty speed { get { return m_speed;}}

        public Bullet(BulletSetUp bulletSetUp, Vector3 target)
        {
            m_target = target;

            UnlimitedProperty atk = new(bulletSetUp.attack, PropertyType.Attack);
            UnlimitedProperty spd = new(bulletSetUp.speed, PropertyType.Speed);
            m_attack = atk;
            m_speed = spd;
        }

        public Bullet(float attack)
        {
            m_attack = new(attack, PropertyType.Attack);
        }
    }
}