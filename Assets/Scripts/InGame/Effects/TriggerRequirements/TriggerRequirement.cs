using System;
using System.Collections.Generic;
using Editors.Board;
using InGame.Boards;
using InGame.Boards.Modules;
using InGame.Boards.Modules.ModuleBuffs;
using UnityEngine.Events;

namespace InGame.Effects.TriggerRequirements
{
    public class RequirementBlackBoard
    {
        public Slot slot;
        public List<ModuleBuff> buffs;
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
        public virtual ModuleBuffType buffMask => ModuleBuffType.None;  
        protected UnityEvent<EffectBlackBoard> m_triggerEvent = new UnityEvent<EffectBlackBoard>();
        protected Module m_module;
        
        public abstract void Register(RequirementBlackBoard bb);

        public abstract void Unregister(RequirementBlackBoard bb);
        
        public void AddBuff(ModuleBuff buff)
        {
            if (ModuleBuff.IsInMask(buffMask, buff.type))
            {
                OnAddBuff(buff);
            }
        }
        
        public virtual void OnAddBuff(ModuleBuff buff){}

        public void RemoveBuff(ModuleBuff buff)
        {
            if (ModuleBuff.IsInMask(buffMask, buff.type))
            {
                OnRemoveBuff(buff);
            }
        }
        
        public virtual void OnRemoveBuff(ModuleBuff buff){}
        
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

