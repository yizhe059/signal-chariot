using InGame.Effects;
using UnityEngine;
using TowerShootEffect = InGame.Effects.EffectElement.TowerShootEffect;

namespace Editors.Effects
{
    public class TowerShootEffectEdit: EffectEdit
    {
        [Min(0.001f)][Tooltip("热量槽极限")]
        public float heatGauge;
        [Min(0f)][Tooltip("散热速度")]
        public float dissipationRate;
        [Min(0f)][Tooltip("每次发射产生的热量")]
        public float heatCostPerShot;
        
        public override Effect CreateEffect()
        {
            if (heatGauge < 0.001f) heatGauge = 0.001f;
            return TowerShootEffect.CreateEffect(heatGauge, dissipationRate, heatCostPerShot);
        }
    }
}