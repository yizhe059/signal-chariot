using UnityEngine;
using World.Signals;

namespace World.Effects
{
    public class SignalActiveGenerationEffect: Effect
    {
        
        private float m_coolDown;
        private int m_signalStrength;
        private Signal.Direction m_dir;
        
        public override void Trigger(EffectBlackBoard blackBoard)
        {
            throw new System.NotImplementedException();
        }

        public override Effect CreateCopy()
        {
            throw new System.NotImplementedException();
        }

        public static SignalActiveGenerationEffect CreateEffect(float coolDown, int strength, Signal.Direction dir)
        {
            return new SignalActiveGenerationEffect
            {
                m_coolDown = coolDown,
                m_signalStrength = strength,
                m_dir = dir
            };
        }
    }
}