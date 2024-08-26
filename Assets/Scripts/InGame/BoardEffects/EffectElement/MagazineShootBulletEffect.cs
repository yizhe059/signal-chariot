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
        public override ModuleBuffType buffMask => ModuleBuffType.Weapon;
        private WeaponBuff m_weaponBuff = WeaponBuff.CreateBuff();
        private List<SignalType> m_bullets = new();
        
        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            //To Do: maybe the trigger requirement is not just the signal
            if (blackBoard.signal == null) return;
            if (blackBoard.signal.type == SignalType.Normal)
            {
                ShootBullet(blackBoard);
            }else if (blackBoard.signal.type != SignalType.None)
            {
                LoadBullet(blackBoard);
            }
        }

        private void ShootBullet(EffectBlackBoard blackBoard)
        {
            GameManager.Instance.GetAndroid().GetTowerManager().
            // TO DO: add the bullet Type to the thing
            TowerEffect(m_module, m_weaponBuff.CreateCopy() as WeaponBuff);
        }

        private void LoadBullet(EffectBlackBoard blackBoard)
        {
            var signal = blackBoard.signal;
            if (m_bullets.Count > magazineCapacity) return;
            
            m_bullets.Add(signal.type);
        }

        public override Effect CreateCopy()
        {
            throw new System.NotImplementedException();
        }
        public override void OnAddBuff(ModuleBuff buff)
        {
            WeaponBuff weaponBuff = (WeaponBuff) buff;
            m_weaponBuff.Add(weaponBuff);
            Debug.Log(m_weaponBuff);
        }
        
        public override void OnRemoveBuff(ModuleBuff buff)
        {
            WeaponBuff weaponBuff = (WeaponBuff) buff;
            m_weaponBuff.Minus(weaponBuff);
            Debug.Log(m_weaponBuff);
        }

        public override void ClearBuffs()
        {
            m_weaponBuff.SetDefault();
            Debug.Log(m_weaponBuff);
        }
        
    }
}