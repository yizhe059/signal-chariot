using System.Collections;

using UnityEngine;

using SetUps;
using InGame.Cores;
using InGame.Views;
using InGame.Boards.Modules;
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
        public BulletManager bulletManager { get { return m_bulletManager;}}
        private BulletSetUp m_bulletSetUp;
        private UnlimitedProperty m_bulletCount;
        private UnlimitedProperty m_shootCount;
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
            UnlimitedProperty bltCnt = new(towerSetUp.bulletCount, UnlimitedPropertyType.BulletCount);
            UnlimitedProperty shtCnt = new(towerSetUp.shootCount);
            UnlimitedProperty shtItv = new(towerSetUp.shootInterval, UnlimitedPropertyType.Interval);
            UnlimitedProperty skItv = new(towerSetUp.seekInterval, UnlimitedPropertyType.Speed);
            UnlimitedProperty dmgMtp = new(towerSetUp.damageMultipler, UnlimitedPropertyType.Multiplier);

            m_bulletManager = new();
            m_bulletSetUp = towerSetUp.bulletSetUp; // TODO: replace this with bullet 

            m_bulletCount = bltCnt;
            m_shootCount = shtCnt;
            m_shootInterval = shtItv;
            m_seekInterval = skItv;

            m_damageMultiplier = dmgMtp;

            m_module = module;              
            m_sprite = towerSetUp.sprite;

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

        #region Effection
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
            BulletSetUp bulletSetUp = m_bulletManager.GenerateBulletSetUp(m_bulletSetUp, buff); // input bulletType & buff
            
            float shootCount = m_shootCount.value + buff.numShotsFlatBuff;
            float bulletCount = m_bulletCount.value + buff.numBulletsPerShotFlatBuff;

            for(int i = 0; i < shootCount; i++)
            {
                m_bulletManager.AddBulletBatch(bulletCount, bulletSetUp, this);
                yield return new WaitForSeconds(m_shootInterval.value);
            }
        }
        #endregion
    }
}