using InGame.Boards.Modules.ModuleBuffs;
using InGame.Cores;
using UnityEngine;

namespace InGame.Effects.EffectElement
{
    public class TowerShootEffect: Effect
    {
        public override ModuleBuffType buffMask => ModuleBuffType.Weapon;
        private WeaponBuff m_buff = WeaponBuff.CreateBuff();
        
        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            GameManager.Instance.GetAndroid().GetTowerManager().TowerEffect(m_module);
        }

        public override void OnAddBuff(ModuleBuff buff)
        {
            WeaponBuff weaponBuff = (WeaponBuff) buff;
            m_buff.Add(weaponBuff);
            Debug.Log(m_buff);
        }
        
        public override void OnRemoveBuff(ModuleBuff buff)
        {
            WeaponBuff weaponBuff = (WeaponBuff) buff;
            m_buff.Minus(weaponBuff);
            Debug.Log(m_buff);
        }

        public override void ClearBuffs()
        {
            m_buff.SetDefault();
            Debug.Log(m_buff);
        }

        public override Effect CreateCopy()
        {
            return new TowerShootEffect();
        }
        

        public static TowerShootEffect CreateEffect()
        {
            return new TowerShootEffect();
        }
    }
}