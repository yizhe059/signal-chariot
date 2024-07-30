using System;

using UnityEngine;

using SetUps;
using InGame.Cores;
using InGame.Views;
using InGame.Boards.Modules;
using InGame.BattleFields.Common;

namespace InGame.BattleFields.Chariots
{
    public enum SeekMode
    {
        None,
        Nearest,
    }

    public class Tower
    {
        [Header("View")]
        private TowerView m_towerView;

        [Header("Properties")]
        private UnlimitedProperty m_damageMultiplier;
        
        [Header("Bullet")]
        private BulletSetUp m_bulletSetUp;
        private UnlimitedProperty m_bulletCount;
        private SeekMode m_seekMode;
        
        [Header("Module")]
        private Module m_module;
        public Module module { get { return m_module;}}

        #region Life Cycle
        public static Tower CreateTower(TowerSetUp towerSetUp, Module module)
        {
            UnlimitedProperty bulletCount = new(towerSetUp.bulletCount, PropertyType.BulletCount);
            UnlimitedProperty attakMultiplier  = new(towerSetUp.damageMultipler, PropertyType.Multiplier);
            
            Tower tower = new()
            {
                m_damageMultiplier = attakMultiplier,
                m_bulletSetUp = towerSetUp.bulletSetUp,
                m_bulletCount = bulletCount,
                m_seekMode = towerSetUp.seekMode,
                m_module = module,               
            };
            
            GameManager.Instance.GetChariot().AddTower(tower);
            tower.CreateView();

            return tower;
        }

        public static void DestroyTower(Tower tower)
        {
            GameManager.Instance.GetChariot().RemoveTower(tower);
        }
        #endregion

        private void CreateView()
        {
            GameObject towerPref = Resources.Load<GameObject>("Prefabs/BattleField/TowerView");
            GameObject towerGO = GameObject.Instantiate(towerPref);
            m_towerView = towerGO.GetComponent<TowerView>();
            m_towerView.Init(this);
        }

        public void Effect()
        {
            ShootBullet();
        }

        private void ShootBullet()
        {
            Vector3 target = FindTarget();
            
            for(int i = 0; i < m_bulletCount.value; i++)
            {
                Bullet bullet = new(m_bulletSetUp, target, m_damageMultiplier.value);
            }
        }

        private Vector3 FindTarget()
        {
            Vector3 target = Vector3.zero;
            switch(m_seekMode)
            {
                case SeekMode.Nearest:
                    // TODO EnemyManager find it
                    break;
                default:
                    break;
            }
            return target;
        }
    }


}