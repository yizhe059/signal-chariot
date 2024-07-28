using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InGame.Cores
{


    // TO DO: Can be a generic class for time type
    public class TimeEffect
    {
        public enum TimeEffectStatus
        {
            InUse,
            Finish
        }
        public float accumulatedTime;
        public float triggerInterval;
        public int maxUsage;
        public int usage;
        public UnityAction<float> action;
        public UnityAction finishAction;
        public TimeEffectStatus status;
    }


    public class TimeEffectManager
    {
        public static readonly int InfiniteUsage = -1;
        
        private float m_lastRecordedTime;

        private int m_triggerInterval;

        private readonly List<TimeEffect> m_effects = new List<TimeEffect>();

        private bool m_isOn = false;

        public void Reset()
        {
            foreach (var timeEffect in m_effects)
            {
                timeEffect.accumulatedTime = timeEffect.triggerInterval;
                timeEffect.usage = timeEffect.maxUsage;
            }

            m_isOn = false;
        }

        public void Start() => m_isOn = true;

        public void Stop() => m_isOn = false;
        
        public void Update(float deltaTime, float newTime)
        {
            if (!m_isOn) return;
            
            for (int i = m_effects.Count - 1; i >= 0; i --)
            {
                var effect = m_effects[i];
                effect.accumulatedTime += deltaTime;
                while (effect.usage != 0 && effect.accumulatedTime >= effect.triggerInterval)
                {
                    effect.accumulatedTime -= effect.triggerInterval;
                    if (effect.usage > 0) effect.usage -= 1;
                    effect.action.Invoke(newTime);
                }

                if (effect.usage == 0)
                {
                    
                    effect.status = TimeEffect.TimeEffectStatus.Finish;
                    effect.finishAction?.Invoke();
                    //don't remove it, let the people who add this time effect decide what to do
                    //m_effects.RemoveAt(i);
                }
                
            }
            
            
        }

        public TimeEffect AddTimeEffect(int usage, float triggerInterval, UnityAction<float> action, UnityAction finishAction)
        {
            Debug.Assert(triggerInterval > 0, $"Invalid Trigger Interval{triggerInterval}");
            var newEffect = new TimeEffect
            {
                maxUsage = usage,
                usage = usage,
                accumulatedTime = 0,
                action = action,
                finishAction = finishAction,
                status = TimeEffect.TimeEffectStatus.InUse,
                triggerInterval = triggerInterval
            };
            
            m_effects.Add(newEffect);
            return newEffect;
        }

        public void RemoveTimeEffect(TimeEffect effect)
        {
            m_effects.Remove(effect);
            effect.status = TimeEffect.TimeEffectStatus.Finish;
        }

        public static TimeEffectManager CreateTimeEffectManager()
        {
            return new TimeEffectManager();
        }
    }   

}