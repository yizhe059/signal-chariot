using UnityEngine;

using InGame.BattleFields.Common;
using InGame.Views;
using SetUps;

namespace InGame.BattleFields.Chariots
{
    public class Bullet
    {   
        [Header("View")]
        private BulletView m_bulletView;

        [Header("Properties")]
        private Vector3 m_target;
        private UnlimitedProperty m_speed;
        private UnlimitedProperty m_damage;

        public Vector3 target { get { return m_target; } }
        public UnlimitedProperty speed { get { return m_speed;}}
        public UnlimitedProperty damage { get { return m_damage;}}

        public Bullet(BulletSetUp bulletSetUp, Vector3 target, float damageMultiplier)
        {
            UnlimitedProperty dmg = new(bulletSetUp.damage * damageMultiplier, PropertyType.Attack);
            UnlimitedProperty spd = new(bulletSetUp.speed, PropertyType.Speed);
            m_target = target;            
            m_damage = dmg;
            m_speed = spd;

            CreateView();
        }

        private void CreateView()
        {
            GameObject bulletPref = Resources.Load<GameObject>("Prefabs/BattleField/BulletView");
            GameObject bulletGO = GameObject.Instantiate(bulletPref);
            m_bulletView = bulletGO.GetComponent<BulletView>();
            m_bulletView.Init(this);
        }
    }
}