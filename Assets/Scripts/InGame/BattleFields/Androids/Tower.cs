using System.Collections;

using UnityEngine;

using SetUps;
using InGame.Cores;
using InGame.Views;
using InGame.Boards.Modules;
using InGame.BattleFields.Enemies;
using InGame.BattleFields.Bullets;
using InGame.BattleFields.Common;

using Utils;
using InGame.Boards.Modules.ModuleBuffs;

namespace InGame.BattleFields.Androids
{
    public class Tower 
    {
        [Header("View")]
        private TowerView m_towerView;
        public TowerView towerView { get {return m_towerView;} }
        private Sprite m_sprite;
        public Sprite sprite { get { return m_sprite; } }

        [Header("Properties")]
        private UnlimitedProperty m_damageMultiplier;
        public UnlimitedProperty damageMultiplier { get { return m_damageMultiplier;}}
        
        [Header("Bullet")]
        private BulletManager m_bulletManager;
        private BulletSetUp m_bulletSetUp;
        private UnlimitedProperty m_bulletCount;
        private UnlimitedProperty m_shootInterval;
        public UnlimitedProperty shootInterval { get { return m_shootInterval;}}
        private UnlimitedProperty m_seekInterval;
        public UnlimitedProperty seekInterval { get { return m_seekInterval;}}
        
        [Header("Module")]
        private Module m_module;
        public Module module { get { return m_module;}}

        #region Life Cycle
        public Tower(TowerSetUp towerSetUp, Module module)
        {
            UnlimitedProperty bulletCount = new(towerSetUp.bulletCount, UnlimitedPropertyType.BulletCount);
            UnlimitedProperty dmgMtp = new(towerSetUp.damageMultipler, UnlimitedPropertyType.Multiplier);
            UnlimitedProperty shtItv = new(towerSetUp.shootInterval, UnlimitedPropertyType.Interval);
            UnlimitedProperty skItv = new(towerSetUp.seekInterval, UnlimitedPropertyType.Speed);
            
            m_bulletCount = bulletCount;
            m_damageMultiplier = dmgMtp;
            m_shootInterval = shtItv;
            m_seekInterval = skItv;

            m_bulletSetUp = towerSetUp.bulletSetUp;
            
            m_module = module;              
            m_sprite = towerSetUp.sprite;

            m_bulletManager = new();
            
            CreateView();
        }

        public void Die()
        {
            m_towerView.Die();
            // m_bulletManager.ClearBullet();
        }
        
        private void CreateView()
        {
            GameObject towerPref = Resources.Load<GameObject>(Constants.GO_TOWER_PATH);
            GameObject towerGO = GameObject.Instantiate(towerPref);
            towerGO.transform.parent = GameManager.Instance.GetAndroid().androidView.transform;
            float x = towerGO.transform.parent.position.x;
            float y = towerGO.transform.parent.position.y;
            towerGO.transform.position = new(x, y, Constants.TOWER_DEPTH); 

            m_towerView = towerGO.GetComponent<TowerView>();
            m_towerView.Init(this);
        }
        #endregion

        public BulletManager GetBulletManager() => m_bulletManager;

        public void Effect()
        {
            m_towerView.Shoot(WeaponBuff.CreateEmptyBuff(ModuleBuffType.Weapon) as WeaponBuff);
        }

        public void Effect(WeaponBuff buff)
        {
            m_towerView.Shoot(buff);
        }

        public IEnumerator ShootBullet(WeaponBuff buff)
        {
            float bulletCount = m_bulletCount.value + buff.numBulletFlatBuff;

            BulletSetUp bulletSetUp = new BulletSetUp(m_bulletSetUp);
            bulletSetUp.bouncingTimes += buff.bouncingBuff;
            bulletSetUp.penetrateTimes += buff.penetrationBuff;
            bulletSetUp.splitTimes += buff.splittingBuff;

            bulletSetUp.speed *= 1 + (float)buff.speedPercentageBuff / 100f;
            bulletSetUp.speed = Mathf.Max(0.001f, bulletSetUp.speed);

            bulletSetUp.damage *= m_damageMultiplier.value;
            bulletSetUp.damage *= 1 + (float)buff.damagePercentageBuff / 100f;
            bulletSetUp.damage += buff.flatDamageBuff;
            bulletSetUp.damage = Mathf.Max(0.001f, bulletSetUp.damage);

            // TODO: bullet size percentage

            bulletSetUp.lifeTime += buff.lifeTimeBuff;

            for(int i = 0; i < bulletCount; i++)
            {
                m_bulletManager.AddBullet(bulletSetUp, this);
                yield return new WaitForSeconds(m_shootInterval.value);
            }
        }
    }
}