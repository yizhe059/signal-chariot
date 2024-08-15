using UnityEngine;

using InGame.BattleFields.Common;
using InGame.Views;
using SetUps;
using Utils;
using InGame.Cores;
using Unity.VisualScripting;
using Utils.Common;

namespace InGame.BattleFields.Bullets
{
    public class Bullet
    {   
        [Header("View")]
        private BulletView m_bulletView;

        [Header("Properties")]
        private Sprite m_sprite;
        private IMoveStrategy m_moveStrategy;
        private DamageType m_damageStrategy;
        private UnlimitedProperty m_speed;
        private UnlimitedProperty m_damage;
        private UnlimitedProperty m_lifeTime;
        private UnlimitedProperty m_reflectTimes;
        private UnlimitedProperty m_penetrateTimes;
        private UnlimitedProperty m_splitTimes;
        
        // TODO DieEffect

        public Sprite sprite{ get { return m_sprite;}}
        public IMoveStrategy moveStrategy { get { return m_moveStrategy;}}
        public DamageType damageType {get { return m_damageStrategy;}}
        public UnlimitedProperty speed { get { return m_speed;}}
        public UnlimitedProperty damage { get { return m_damage;}}
        public UnlimitedProperty lifetime { get { return m_lifeTime;}}
        public UnlimitedProperty reflectTimes { get { return m_reflectTimes;}}
        public UnlimitedProperty penetrateTimes { get {return m_penetrateTimes;}}
        public UnlimitedProperty splitTimes { get {return m_splitTimes;}}

        public Bullet(BulletSetUp bulletSetUp, float damageMultiplier)
        {
            UnlimitedProperty dmg = new(bulletSetUp.damage * damageMultiplier, 
                                        UnlimitedPropertyType.Damage);
            UnlimitedProperty spd = new(bulletSetUp.speed, UnlimitedPropertyType.Speed);

            m_damage = dmg;
            m_speed = spd;
            m_sprite = bulletSetUp.sprite;

            CreateView();
        }

        private void CreateView()
        {
            GameObject bulletPref = Resources.Load<GameObject>(Constants.GO_BULLET_PATH);
            GameObject bulletGO = GameObject.Instantiate(bulletPref);
            
            float x = GameManager.Instance.GetAndroid().androidView.transform.position.x;
            float y = GameManager.Instance.GetAndroid().androidView.transform.position.y;

            bulletGO.transform.position = new(x, y, Constants.BULLET_DEPTH);

            m_bulletView = bulletGO.GetComponent<BulletView>();
            m_bulletView.Init(this);
        }

        public void Die()
        {
            m_bulletView.Die();
        }

        public void DealDamage(IDamageable target, float dmg)
        {
            target.TakeDamage(dmg);
            this.Die();
        }
    }
}