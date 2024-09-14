using InGame.Boards.Modules.ModuleBuffs;
using InGame.Boards.Signals;
using InGame.Cores;
using Unity.VisualScripting;
using UnityEngine;

namespace InGame.Effects.EffectElement
{
    public class WeaponShootEffect: Effect
    {
        public override ModuleBuffType buffMask => ModuleBuffType.Weapon;
        public float heatGauge;
        public float dissipationRate;
        public float heatCostPerShot;
        public int electricCapacity;
        
        private int m_currentElectricCharge = 0;
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

        private void IncreaseElectricCharge(int delta)
        {
            m_currentElectricCharge += delta;
            m_currentElectricCharge = Mathf.Clamp(m_currentElectricCharge, 0, electricCapacity);
        }

        private void ClearElectricCharge()
        {
            m_currentElectricCharge = 0;
        }

        private int GetCurrentElectricCharge() => m_currentElectricCharge;
        

        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            
            UpdateHeat(blackBoard.time.val);
            
            if (m_isOverHeated) return;

            var signalType = blackBoard.signal.type;
            var bulletType = Signal.SignalTypeToBulletType(signalType);
            
            AddHeat(heatCostPerShot);

            int bulletLevel = 1;
            if (GetCurrentElectricCharge() == electricCapacity)
            {
                bulletLevel++;
                ClearElectricCharge();
            }
            else
            {
                IncreaseElectricCharge(1);
            }
            
            GameManager.Instance.GetAndroid().GetEquipmentManager().
            EquipmentEffect(m_module, bulletType, bulletLevel, m_buff.CreateCopy() as WeaponBuff);
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
            return new WeaponShootEffect
            {
                heatGauge = heatGauge,
                dissipationRate = dissipationRate,
                heatCostPerShot = heatCostPerShot,
                electricCapacity = electricCapacity
            };
        }
        

        public static WeaponShootEffect CreateEffect(float heatGauge, float dissipationRate, float heatCostPerShot, int electricCapacity)
        {
            return new WeaponShootEffect
            {
                heatGauge = heatGauge,
                dissipationRate = dissipationRate,
                heatCostPerShot = heatCostPerShot,
                electricCapacity = electricCapacity
            };
        }
    }
}