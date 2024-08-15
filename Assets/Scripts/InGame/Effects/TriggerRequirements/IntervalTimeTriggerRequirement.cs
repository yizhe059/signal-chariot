using InGame.Boards;
using InGame.Cores;


namespace InGame.Effects.TriggerRequirements
{
    [System.Serializable]
    public class IntervalTimeTriggerRequirement: TriggerRequirement
    {
        public float interval = 1f;
        public bool canEffectInTest = false;
        private float m_accumulatedTime = 0f;
        private UpdateEffect m_effect;
        private BoardPosition m_modulePos;

        public override TriggerType type => TriggerType.Time;
        
        public override void Register(RequirementBlackBoard bb)
        {
            m_effect = GameManager.Instance.GetTimeEffectManager().AddUpdateEffect(OnUpdate, canEffectInTest);
            m_modulePos = bb.slot.pos;
            m_accumulatedTime = interval;
        }

        public override void Unregister(RequirementBlackBoard bb)
        {
            GameManager.Instance.GetTimeEffectManager().RemoveUpdateEffect(m_effect);
            m_effect = null;
            
        }

        public override TriggerRequirement CreateCopy()
        {
            return new IntervalTimeTriggerRequirement
            {
                interval = interval,
                canEffectInTest = canEffectInTest,
                m_accumulatedTime = 0,
                m_effect = null,
                
            };
        }

        private void OnUpdate(float deltaTime)
        {

            m_accumulatedTime += deltaTime;

            while (m_accumulatedTime >= interval)
            {
                m_triggerEvent.Invoke(new EffectBlackBoard
                {
                    triggerType = type,
                    slot = GameManager.Instance.GetBoard().GetValue(m_modulePos.x, m_modulePos.y)
                });
                m_accumulatedTime -= interval;
            }
        }

        public static IntervalTimeTriggerRequirement CreateRequirement(float interval, bool canEffectInTest)
        {
            return new IntervalTimeTriggerRequirement
            {
                interval = interval,
                canEffectInTest = canEffectInTest
            };
        }
    }
}