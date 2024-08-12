using UnityEngine.Events;

namespace InGame.Effects.TriggerRequirements
{
    public class RequirementBlackBoard
    {
        
    }
    
    public abstract class TriggerRequirement
    {
        private UnityEvent<EffectBlackBoard> m_triggerEvent = new UnityEvent<EffectBlackBoard>();
        public abstract void Register(RequirementBlackBoard bb);

        public abstract void Unregister(RequirementBlackBoard bb);

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

