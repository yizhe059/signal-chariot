using InGame.Boards;
using InGame.Boards.Signals;
using InGame.Cores;
using SetUps;
using UnityEngine;

namespace InGame.Effects.EffectElement
{
    public class SignalActiveGenerationEffect: Effect
    {
        
        [SerializeField]
        private float m_coolDown;
        [SerializeField]
        private int m_signalStrength;
        
        
        [SerializeField]
        private Signal.Direction m_dir;
        
        private TimeEffect m_createSignalTimeEffect;
        
        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            var timeEffectManager = GameManager.Instance.GetTimeEffectManager();
            m_createSignalTimeEffect = timeEffectManager.AddTimeEffect(
                TimeEffectManager.InfiniteUsage,
                m_coolDown,
                (time) =>
                {
                    CreateSignal(new BoardPosition(blackBoard.slot.pos));
                },
                () => { m_createSignalTimeEffect = null; },
                true);
        }

        public override void OnUnTrigger(EffectBlackBoard blackBoard)
        {
            Debug.Assert(m_createSignalTimeEffect != null);
            var manager = GameManager.Instance.GetTimeEffectManager();
            manager.RemoveTimeEffect(m_createSignalTimeEffect);
            m_createSignalTimeEffect = null;
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

        private void CreateSignal(BoardPosition pos)
        {

            Debug.Assert(m_module != null);
            var signalController = GameManager.Instance.GetSignalController();

            Debug.Assert(signalController != null);
            var orientation = m_module.orientation;
            
            signalController.CreateSignal(new SignalSetUp
            {
                dir = Signal.OrientationToDirection(orientation, m_dir),
                energy = m_signalStrength,
                pos = pos
            });
            
            
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