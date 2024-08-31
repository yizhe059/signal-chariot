using System.Collections.Generic;
using InGame.Boards.Signals;
using InGame.Cores;
using SetUps;
using UnityEngine;

namespace InGame.Effects.EffectElement
{
    public class SplitSignalEffect: Effect
    {
        protected override bool canEffectByTest => true;
        public List<Signal.Direction> splittingDirections;
        
        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            var signal = blackBoard.signal;
            if (signal == null) return;
            var inputDir = Signal.OrientationToDirection(m_module.orientation, Signal.Direction.Up);
            if (Signal.ReverseDirection(inputDir) != signal.dir) return;
            
            var count = splittingDirections.Count;
            if (splittingDirections.Count == 0) return;

            var signalType = signal.type;
            int energy = signal.energy;
            signal.ConsumeEnergy(energy);
            
            int extraEnergy = count - (energy % count);
            if (extraEnergy == count) extraEnergy = 0;
            
            energy += extraEnergy;

            int energyPerSignal = energy / count;

            foreach (var dir in splittingDirections)
            {
                var worldDir = Signal.OrientationToDirection(m_module.orientation, dir);
                GameManager.Instance.GetSignalController().CreateSignal(new SignalSetUp
                {
                    dir = worldDir,
                    energy = energyPerSignal,
                    pos = blackBoard.pos,
                    
                }, 0, signalType);
            }
        }

        public override Effect CreateCopy()
        {
            return new SplitSignalEffect
            {
                splittingDirections = new List<Signal.Direction>(splittingDirections)
            };
        }

        public static Effect CreateEffect(List<Signal.Direction> dirs)
        {
            return new SplitSignalEffect
            {
                splittingDirections = dirs
            };
        }
    }
}