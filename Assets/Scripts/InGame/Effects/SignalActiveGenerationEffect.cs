using InGame.Boards.Signals;
using InGame.Cores;

namespace InGame.Effects
{
    public class SignalActiveGenerationEffect: Effect
    {
        
        private float m_coolDown;
        private int m_signalStrength;
        private Signal.Direction m_dir;
        private TimeEffect m_createSignalTimeEffect;
        
        public override void Trigger(EffectBlackBoard blackBoard)
        {
            var timeEffectManager = GameManager.Instance.GetTimeEffectManager();
            m_createSignalTimeEffect = timeEffectManager.AddTimeEffect(
                TimeEffectManager.InfiniteUsage, 
                m_coolDown,
                null, null);
        }

        public override Effect CreateCopy()
        {
            return new SignalActiveGenerationEffect
            {
                m_coolDown = m_coolDown,
                m_signalStrength = m_signalStrength,
                m_dir = m_dir
            };
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