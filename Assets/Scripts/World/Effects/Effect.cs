using System.Collections.Generic;
using Unity.VisualScripting;
using World.Signals;

namespace World.Effects
{
    public class EffectBlackBoard
    {
        public Signal signal;
        public Time time;

        public void Clean()
        {
            signal = null;
            
        }
    }
    [System.Serializable]
    public abstract class Effect
    {
        public abstract void Trigger(EffectBlackBoard blackBoard);

        public virtual void UnTrigger(EffectBlackBoard blackBoard)
        {
        }

        public abstract Effect CreateCopy();
    }

    public class SignalEffects
    {
        private List<Effect> m_effects;
        private int m_maxUses, m_remainUses;
        private int m_energyConsumption;
        
        private Time m_coolDown;
        private Time m_prevTriggerTime;

        public static SignalEffects CreateSignalEffects(List<Effect> signalEffects, int maxUses, int energyConsumption,
            float coolDown)
        {
            return new SignalEffects
            {
                m_effects = new List<Effect>(signalEffects),
                m_maxUses = maxUses,
                m_remainUses = maxUses,
                m_energyConsumption = energyConsumption,
                m_coolDown = new Time(coolDown),
                m_prevTriggerTime = new Time(-coolDown)
            };
        }
        
        public static SignalEffects CreateSignalEffects(List<Effect> signalEffects, int maxUses, int energyConsumption,
            Time coolDown)
        {
            return new SignalEffects
            {
                m_effects = new List<Effect>(signalEffects),
                m_maxUses = maxUses,
                m_remainUses = maxUses,
                m_energyConsumption = energyConsumption,
                m_coolDown = coolDown,
                m_prevTriggerTime = -coolDown
            };
        }

        public static SignalEffects CreateSignalEffects(SignalEffects other)
        {
            var signalEffects = new List<Effect>();
            foreach (var eff in other.m_effects)
            {
                signalEffects.Add(eff.CreateCopy());
            }


            return CreateSignalEffects(signalEffects, other.m_maxUses, other.m_energyConsumption, other.m_coolDown);
        }

        public void Trigger(EffectBlackBoard blackBoard)
        {
            var signal = blackBoard.signal;
            var time = blackBoard.time;
            // cool down not finished
            if (time - m_prevTriggerTime < m_coolDown) return;

            if (m_maxUses != -1 && m_remainUses <= 0) return;

            if (signal.energy < m_energyConsumption) return;

            m_remainUses--;
            signal.ConsumeEnergy(m_energyConsumption);
            m_prevTriggerTime = time;
            
            foreach (var effect in m_effects)
            {
                effect.Trigger(blackBoard);
            }
            
        }
    }


    public class PlacingEffects
    {
        private List<Effect> m_effects;

        public static PlacingEffects CreatePlacingEffects(List<Effect> effect)
        {
            return new PlacingEffects
            {
                m_effects = new List<Effect>(effect)
            };
        }
        
        public static PlacingEffects CreatePlacingEffects(PlacingEffects other)
        {
            var placingEffects = new List<Effect>();
            foreach (var eff in other.m_effects)
            {
                placingEffects.Add(eff.CreateCopy());
            }
            return new PlacingEffects
            {
                m_effects = placingEffects
            };
        }
        
        public void Trigger(EffectBlackBoard blackBoard)
        {
            foreach (var effect in m_effects)
            {
                effect.Trigger(blackBoard);
            }
        }

        public void UnTrigger(EffectBlackBoard blackBoard)
        {
            foreach (var effect in m_effects)
            {
                effect.UnTrigger(blackBoard);
            }
        }
    }
}