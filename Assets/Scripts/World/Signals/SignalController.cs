using System.Collections.Generic;
using Utils;
using World.Cores;
using World.Effects;
using World.Views;

namespace World.Signals
{

    
    
    public class SignalController
    {
        private class SignalPack
        {
            public float timer;
            public Signal signal;
            public bool isDead = false;
            public EffectBlackBoard blackBoard;
        }
        
        private List<SignalPack> m_signals = new List<SignalPack>();
        private bool m_isOn = false;
        private Board m_board;
        private BoardView m_boardView;

        public static SignalController CreateSignalController(Board board)
        {
            return new SignalController {m_board = board}; 
        }

        public Signal CreateSignal(SignalSetUp setUp)
        {
            var id = m_signals.Count;
            setUp.id = id;

            var signal = Signal.CreateSignal(setUp);
            var blackBoard = new EffectBlackBoard
            {
                signal = signal,
                time = new Time(0f)
            };
            
            var signalPack = new SignalPack
            {
                signal = signal,
                timer = 0f
            };
            
            m_signals.Add(signalPack);
            return signal;
        }

        public void Reset()
        {
            foreach (var signalPack in m_signals)
            {
                signalPack.timer = 0;
            }
        }

        public void Start()
        {
            m_isOn = true;

            foreach (var signalPack in m_signals)
            {
                m_boardView.CreateSignalView(signalPack.signal);
            }
        }

        public void Update(float deltaTime)
        {
            if (!m_isOn) return;

            foreach (var signalPack in m_signals)
            {
                signalPack.timer += deltaTime;
                signalPack.blackBoard.time += new Time(deltaTime);

                while (signalPack.timer >= Constants.SIGNAL_MOVING_DURATION)
                {
                    
                    signalPack.timer -= Constants.SIGNAL_MOVING_DURATION;
                    MoveSignal(signalPack.signal, signalPack.blackBoard, out signalPack.isDead);
                }
                
                
            }
        }

        private void MoveSignal(Signal signal, EffectBlackBoard bb, out bool isDead)
        {
            var pos = signal.pos;
            var dir = signal.dir;
            var newPos = pos + Signal.GetDirVector(dir);
            bool isInBound = m_board.GetSlotStatus(newPos, out var newPosStatus);
            
            // can not move out of board or unactive region
            if (!isInBound || (newPosStatus != SlotStatus.Occupied && newPosStatus != SlotStatus.Empty))
            {
                isDead = true;
                return;
            }
            
            signal.SetPos(newPos);
            
            m_board.TriggerEffect(newPos.x, newPos.y, bb);

            if (signal.energy <= 0)
            {
                isDead = true;
            }
            else
            {
                isDead = false;
            }
            
        }
    }
}