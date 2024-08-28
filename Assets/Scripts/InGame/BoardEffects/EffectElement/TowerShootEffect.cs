using InGame.Boards.Modules.ModuleBuffs;
using InGame.Cores;
using Unity.VisualScripting;
using UnityEngine;

namespace InGame.Effects.EffectElement
{
    public class TowerShootEffect: Effect
    {
        public override ModuleBuffType buffMask => ModuleBuffType.Weapon;
        public float heatGauge;
        public float dissipationRate;
        public float heatCostPerShot;

        private float m_currentHeat = 0;
        private bool m_isOverHeated = false;
        private float m_lastRecordedTime = -1f;
        private WeaponBuff m_buff = WeaponBuff.CreateBuff();

        private void AddHeat(float delta)
        {
            m_currentHeat += delta;
            if (m_currentHeat >= heatGauge)
            {
                m_currentHeat = heatGauge;
                m_isOverHeated = true;
            }
        }

        private void ReduceHeat(float delta)
        {
            m_currentHeat -= delta;
            if (m_currentHeat <= 0)
            {
                m_currentHeat = 0;
                m_isOverHeated = false;
            }
        }

        private void UpdateHeat(float currentTime)
        {
            float delta = currentTime - m_lastRecordedTime;
            m_lastRecordedTime = currentTime;
            if (m_currentHeat <= Mathf.Epsilon) return;
            Debug.Assert(delta >= 0);

            float dissipationEnergy = dissipationRate * delta;
            ReduceHeat(dissipationEnergy);
        }
        
        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            
            UpdateHeat(blackBoard.time.val);
            
            if (m_isOverHeated) return;
            AddHeat(heatCostPerShot);
            GameManager.Instance.GetAndroid().GetTowerManager().
            TowerEffect(m_module, m_buff.CreateCopy() as WeaponBuff);
        }

        protected override void OnReset()
        {
            m_currentHeat = 0f;
            m_isOverHeated = false;
            m_lastRecordedTime = -1f;
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
            return new TowerShootEffect
            {
                heatGauge = heatGauge,
                dissipationRate = dissipationRate,
                heatCostPerShot = heatCostPerShot,
            };
        }
        

        public static TowerShootEffect CreateEffect(float heatGauge, float dissipationRate, float heatCostPerShot)
        {
            return new TowerShootEffect
            {
                heatGauge = heatGauge,
                dissipationRate = dissipationRate,
                heatCostPerShot = heatCostPerShot,
            };
        }
    }
}