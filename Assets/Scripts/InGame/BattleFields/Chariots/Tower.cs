using System.Threading.Tasks;

using UnityEngine;

using SetUps;
using InGame.Cores;
using InGame.Views;
using InGame.Boards.Modules;
using InGame.BattleFields.Common;
using Utils;

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
        private UnlimitedProperty m_bulletShootInterval;
        private SeekMode m_seekMode;
        
        [Header("Module")]
        private Module m_module;
        public Module module { get { return m_module;}}

        #region Life Cycle
        public Tower(TowerSetUp towerSetUp, Module module)
        {
            UnlimitedProperty bulletCount = new(towerSetUp.bulletCount, UnlimitedPropertyType.BulletCount);
            UnlimitedProperty attakMultiplier  = new(towerSetUp.damageMultipler, UnlimitedPropertyType.Multiplier);
            UnlimitedProperty bulletShootInterval = new(towerSetUp.bulletShootInterval, UnlimitedPropertyType.Interval);
            
            m_damageMultiplier = attakMultiplier;
            m_bulletSetUp = towerSetUp.bulletSetUp;
            m_bulletCount = bulletCount;
            m_bulletShootInterval = bulletShootInterval;
            m_seekMode = towerSetUp.seekMode;
            m_module = module;              
            
            CreateView();
        }

        public void Die()
        {
            
        }
        
        private void CreateView()
        {
            GameObject towerPref = Resources.Load<GameObject>(Constants.GO_TOWER_PATH);
            GameObject towerGO = GameObject.Instantiate(towerPref);
            towerGO.transform.parent = GameManager.Instance.GetChariot().chariotView.transform;
            float x = towerGO.transform.parent.position.x;
            float y = towerGO.transform.parent.position.y;
            towerGO.transform.position = new(x, y, Constants.TOWER_DEPTH); 

            m_towerView = towerGO.GetComponent<TowerView>();
            m_towerView.Init(this);
        }
        #endregion

        public async void Effect()
        {
            await ShootBullet();
        }

        private async Task ShootBullet()
        {
            Vector3 target = FindTarget();
            
            for(int i = 0; i < m_bulletCount.value; i++)
            {
                new Bullet(m_bulletSetUp, target, m_damageMultiplier.value);
                float intervalInMS = m_bulletShootInterval.value * 1000;
                await Task.Delay((int)intervalInMS);
            }
        }

        private Vector3 FindTarget()
        {
            Vector3 target = Vector3.zero;
            switch(m_seekMode)
            {
                case SeekMode.Nearest:
                    target = new(10, 10, 0);
                    break;
                default:
                    break;
            }
            return target;
        }
    }


}