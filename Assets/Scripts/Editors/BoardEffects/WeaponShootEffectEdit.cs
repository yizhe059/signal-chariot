using InGame.Effects;
using UnityEngine;
using WeaponShootEffect = InGame.Effects.EffectElement.WeaponShootEffect;

namespace Editors.Effects
{
    public class WeaponShootEffectEdit: EffectEdit
    {
        [Min(0.001f)][Tooltip("热量槽极限")]
        public float heatGauge;
        [Min(0f)][Tooltip("散热速度")]
        public float dissipationRate;
        [Min(0f)][Tooltip("每次发射产生的热量")]
        public float heatCostPerShot;
        [Min(0f)][Tooltip("电荷容量。电荷容量为0时代表永远为打出第二级子弹")]
        public int electricCapacity;
        
        public override Effect CreateEffect()
        {
            if (heatGauge < 0.001f) heatGauge = 0.001f;
            return WeaponShootEffect.CreateEffect(heatGauge, dissipationRate, heatCostPerShot, electricCapacity);
        }
    }
}