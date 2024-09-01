using System.Collections.Generic;
using InGame.Boards.Modules.ModuleBuffs;
using InGame.Boards.Signals;
using InGame.Cores;
using UnityEngine;

namespace InGame.Effects.EffectElement
{
    public class MagazineShootBulletEffect: Effect
    {
        public int magazineCapacity;
        public override ModuleBuffType buffMask => ModuleBuffType.Weapon | ModuleBuffType.Magazine;
        private WeaponBuff m_weaponBuff = WeaponBuff.CreateBuff();
        private MagazineBuff m_magazineBuff = MagazineBuff.CreateBuff();
        private Queue<SignalType> m_bullets = new();
        
        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            //To Do: maybe the trigger requirement is not just the signal
            if (blackBoard.signal == null) return;
            // if (blackBoard.signal.type == SignalType.Normal)
            // {
            //     ShootBullet(blackBoard);
            // }else if (blackBoard.signal.type != SignalType.None)
            // {
            //     LoadBullet(blackBoard);
            // }
            LoadBullet(blackBoard);
            if (m_bullets.Count == magazineCapacity + m_magazineBuff.magazineCapacityBuff)
            {
                ShootBullets();
            }
        }

        private void ShootBullets()
        {
            var towerManager = GameManager.Instance.GetAndroid().GetTowerManager();
            while (m_bullets.Count != 0)
            {
                var type = m_bullets.Dequeue();
                
                var bulletType = Signal.SignalTypeToBulletType(type);
                Debug.Log($"Shoot Bullet： {bulletType}");
                towerManager.TowerEffect(m_module, bulletType, m_weaponBuff.CreateCopy() as WeaponBuff);
            }
        }
        
        private void ShootBullet(EffectBlackBoard blackBoard)
        {
            if (m_bullets.Count == 0) return;
            var bullet = m_bullets.Dequeue();
            var towerManager = GameManager.Instance.GetAndroid().GetTowerManager();
            
            Debug.Log($"Shoot Bullet： {bullet}");
            // TO DO: add the bullet Type to the thing
            //towerManager.TowerEffect(m_module, m_weaponBuff.CreateCopy() as WeaponBuff);
        }

        private void LoadBullet(EffectBlackBoard blackBoard)
        {
            var signal = blackBoard.signal;
            
            if(m_bullets.Count < magazineCapacity + m_magazineBuff.magazineCapacityBuff)
            {
                m_bullets.Enqueue(signal.type);
                Debug.Log($"Load Bullet {signal.type}");
            }
            
            
        }

        
        
        public override void OnAddBuff(ModuleBuff buff)
        {
            if (buff.type == ModuleBuffType.Weapon)
            {
                WeaponBuff weaponBuff = (WeaponBuff) buff;
                m_weaponBuff.Add(weaponBuff);
                Debug.Log(m_weaponBuff);
            }else if (buff.type == ModuleBuffType.Magazine)
            {
                MagazineBuff magazineBuff = (MagazineBuff) buff;
                m_magazineBuff.Add(magazineBuff);
            }
            
            
        }
        
        public override void OnRemoveBuff(ModuleBuff buff)
        {
            if (buff.type == ModuleBuffType.Weapon)
            {
                WeaponBuff weaponBuff = (WeaponBuff) buff;
                m_weaponBuff.Minus(weaponBuff);
                Debug.Log(m_weaponBuff);
            }else if (buff.type == ModuleBuffType.Magazine)
            {
                MagazineBuff magazineBuff = (MagazineBuff) buff;
                m_magazineBuff.Minus(magazineBuff);
            }
            
        }

        public override void ClearBuffs()
        {
            m_weaponBuff.SetDefault();
            m_magazineBuff.SetDefault();
            Debug.Log(m_weaponBuff);
        }
        
        public override Effect CreateCopy()
        {
            return new MagazineShootBulletEffect
            {
                magazineCapacity = magazineCapacity
            };
        }

        public static Effect CreateEffect(int capacity)
        {
            return new MagazineShootBulletEffect
            {
                magazineCapacity = capacity
            };
        }
        
    }
}