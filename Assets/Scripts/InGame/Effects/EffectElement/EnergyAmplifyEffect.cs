using UnityEngine;

namespace InGame.Effects.EffectElement
{
    public class EnergyAmplifyEffect: Effect
    {
        public int amplifiedAmount;
        
        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            var signal = blackBoard.signal;
            Debug.Assert(signal != null);
            
            signal.IncreaseEnergy(amplifiedAmount);

        }

        public override Effect CreateCopy()
        {
            return new EnergyAmplifyEffect
            {
                amplifiedAmount = amplifiedAmount
            };
        }

        public static EnergyAmplifyEffect CreateEffect(int amount)
        {
            return new EnergyAmplifyEffect
            {
                amplifiedAmount = amount
            };
            
        }
    }
}