using UnityEngine;

using InGame.BattleFields.Common;
using InGame.Views;
using SetUps;
using Utils;

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
            UnlimitedProperty dmg = new(bulletSetUp.damage * damageMultiplier, UnlimitedPropertyType.Attack);
            UnlimitedProperty spd = new(bulletSetUp.speed, UnlimitedPropertyType.Speed);
            m_target = target;            
            m_damage = dmg;
            m_speed = spd;

            CreateView();
        }

        private void CreateView()
        {
            GameObject bulletPref = Resources.Load<GameObject>(Constants.GO_BULLET_PATH);
            GameObject bulletGO = GameObject.Instantiate(bulletPref);
            bulletGO.transform.position = new(0, 0, Constants.BULLET_DEPTH);

            m_bulletView = bulletGO.GetComponent<BulletView>();
            m_bulletView.Init(this);
        }
    }
}