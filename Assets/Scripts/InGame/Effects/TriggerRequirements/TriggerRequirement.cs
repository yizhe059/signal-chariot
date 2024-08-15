using System;
using InGame.Boards;
using InGame.Boards.Modules;
using UnityEngine.Events;

namespace InGame.Effects.TriggerRequirements
{
    public class RequirementBlackBoard
    {
        public Slot slot;
    }

    public enum TriggerType
    {
        None,
        Time
    }
    
    [Serializable]
    public abstract class TriggerRequirement
    {
        public abstract TriggerType type { get; }
        protected UnityEvent<EffectBlackBoard> m_triggerEvent = new UnityEvent<EffectBlackBoard>();
        protected Module m_module;
        
        public abstract void Register(RequirementBlackBoard bb);

        public abstract void Unregister(RequirementBlackBoard bb);

        public void SetModule(Module module)
        {
            m_module = module;
        }
        public void RegisterTriggerEvent(UnityAction<EffectBlackBoard> act)
        {
            m_triggerEvent.AddListener(act);
        }

        public void UnregisterTriggerEvent(UnityAction<EffectBlackBoard> act)
        {
            m_triggerEvent.RemoveListener(act);
        }

        public abstract TriggerRequirement CreateCopy();

    }
}

