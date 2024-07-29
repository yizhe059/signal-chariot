using SetUps;
using InGame.Cores;
using InGame.Views;
using InGame.Boards.Modules;

using UnityEngine;

namespace InGame.BattleFields.Chariots
{
    public enum SeekMode
    {
        None,
        Nearest,
    }

    public class Tower
    {
        private TowerView m_towerView;

        private BulletSetUp m_bulletSetUp;
        private UnlimitedProperty m_bulletCount;
        private SeekMode m_seekMode;
        private UnlimitedProperty m_attackMultiplier;
        private Module m_module;

        public static Tower CreateTower(TowerSetUp towerSetUp, Module module)
        {
            UnlimitedProperty bulletCount = new(towerSetUp.bulletCount, PropertyType.BulletCount);
            
            Tower tower = new()
            {
                m_bulletSetUp = towerSetUp.bulletSetUp,
                m_bulletCount = bulletCount,
                m_seekMode = towerSetUp.seekMode,
                m_module = module
            };
            
            GameManager.Instance.GetChariot().AddTower(tower);

            return tower;
        }

        public static void DestroyTower(Tower tower)
        {
            GameManager.Instance.GetChariot().RemoveTower(tower);
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
                new Bullet(m_bulletSetUp, target);
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