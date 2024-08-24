using InGame.Boards.Signals;
using InGame.Cores;
using SetUps;
using UnityEngine;


namespace InGame.Effects.EffectElement
{
    public class SignalGenerationEffect: Effect
    {
        public int signalStrength = 1;
        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            Debug.Assert(m_module != null);
            var signalController = GameManager.Instance.GetSignalController();
            var orientation = m_module.orientation;
            
            signalController.CreateSignal(new SignalSetUp
            {
                dir = Signal.OrientationToDirection(orientation, Signal.Direction.Up),
                energy = signalStrength,
                pos = blackBoard.slot.pos
            });
        }

        public override Effect CreateCopy()
        {
            return new SignalGenerationEffect
            {
                signalStrength = signalStrength
            };
        }

        public static Effect CreateEffect(int strength)
        {
            return new SignalGenerationEffect
            {
                signalStrength = strength
            };
        }
        
    }
}