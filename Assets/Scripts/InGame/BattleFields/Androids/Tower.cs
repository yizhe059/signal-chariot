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
        private BulletType m_bulletType;
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
        public Tower(EquipmentSetUp equipmentSetUp, Module module)
        {
            UnlimitedProperty bltCnt = new(equipmentSetUp.bulletCount, UnlimitedPropertyType.BulletCount);
            UnlimitedProperty shtCnt = new(equipmentSetUp.shootCount);
            UnlimitedProperty shtItv = new(equipmentSetUp.shootInterval, UnlimitedPropertyType.Interval);
            UnlimitedProperty skItv = new(equipmentSetUp.seekInterval, UnlimitedPropertyType.Speed);
            UnlimitedProperty dmgMtp = new(equipmentSetUp.damageMultipler, UnlimitedPropertyType.Multiplier);

            m_bulletManager = new();
            m_bulletType = equipmentSetUp.bulletType; 

            m_bulletCount = bltCnt;
            m_shootCount = shtCnt;
            m_shootInterval = shtItv;
            m_seekInterval = skItv;

            m_damageMultiplier = dmgMtp;

            m_module = module;              
            m_sprite = equipmentSetUp.sprite;

            CreateView();
        }

        public void Die()
        {
            m_towerView.Die();
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

        public void Effect(WeaponBuff buff, BulletType type)
        {
            m_towerView.Shoot(buff, type);
        }

        public IEnumerator ShootBullet(WeaponBuff buff, BulletType type)
        {
            BulletSetUp bulletSetUp = m_bulletManager.GenerateBulletSetUp(type, buff);
            
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