using InGame.Boards.Signals;
using InGame.Cores;
using SetUps;
using UnityEngine;

namespace InGame.Effects.EffectElement
{
    public class SignalGenerationFromUsedEnergyEffect: Effect
    {
        protected override bool canEffectByTest => true;

        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            if (blackBoard.energyUsed == 0)
            {
                return;
            }
            var signalController = GameManager.Instance.GetSignalController();
            var orientation = m_module.orientation;
            
            signalController.CreateSignal(new SignalSetUp
            {
                dir = Signal.OrientationToDirection(orientation, Signal.Direction.Up),
                energy = blackBoard.energyUsed,
                pos = blackBoard.pos
            });
        }

        public override Effect CreateCopy()
        {
            return new SignalGenerationFromUsedEnergyEffect();
        }

        public static Effect CreateEffect()
        {
            return new SignalGenerationFromUsedEnergyEffect();
        }
    }
}