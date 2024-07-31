using System.Collections.Generic;
using InGame.Effects;
using InGame.Views;
using SetUps;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace InGame.Boards.Signals
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
        private bool m_isTest = false;
        private UnityEvent m_onNoSignal = new UnityEvent();
        
        private Board m_board;
        private BoardView m_boardView;

        public static SignalController CreateSignalController(Board board, BoardView boardView)
        {
            return new SignalController {m_board = board, m_boardView = boardView}; 
        }

        public Signal CreateSignal(SignalSetUp setUp)
        {
            
            var id = -1;
            for (int i = 0; i < m_signals.Count; i++)
            {
                if (m_signals[i] == null)
                {
                    id = i;
                    break;
                }
            }

            if (id == -1)
            {
                id = m_signals.Count;
                m_signals.Add(null);
            }
            setUp.id = id;

            var signal = Signal.CreateSignal(setUp);
            var blackBoard = new EffectBlackBoard
            {
                signal = signal,
                time = new Time(0f),
                isTest = m_isTest
            };
            
            var signalPack = new SignalPack
            {
                signal = signal,
                timer = 0f,
                blackBoard = blackBoard
            };


            m_signals[id] = signalPack;
            m_boardView.CreateSignalView(signalPack.signal);
            if (m_isOn) signal.Start();
            else signal.Stop();
            
            return signal;
        }

        public void RemoveSignal(int id)
        {
            var signalPack = m_signals[id];

            if (signalPack == null) return;

            m_signals[id] = null;
            
            signalPack.signal.SelfDestroy();
            signalPack.blackBoard.Clean();
        }
        
        public void Reset()
        {
            foreach (var signalPack in m_signals)
            {
                if (signalPack == null) continue;
                signalPack.timer = 0;
            }
        }

        public void Start()
        {
            m_isOn = true;
            m_isTest = false;

            foreach (var signalPack in m_signals)
            {
                if (signalPack == null) continue;
                signalPack.blackBoard.isTest = m_isTest;
                signalPack.signal.Start();
            }
        }
        
        public void Stop()
        {
            m_isOn = false;
            m_isTest = false;
            for (int i = 0; i < m_signals.Count; i++)
            {
                RemoveSignal(i);
            }
        }
        
        public void TestStart(UnityAction noSignalCallBack)
        {
            m_isOn = true;
            m_isTest = true;
            m_onNoSignal.AddListener(noSignalCallBack);
            foreach (var signalPack in m_signals)
            {
                if (signalPack == null) continue;
                signalPack.blackBoard.isTest = m_isTest;
                signalPack.signal.Start();
            }
        }

        public void TestStop(UnityAction noSignalCallBack)
        {
            m_isOn = false;
            m_isTest = false;
            m_onNoSignal.RemoveListener(noSignalCallBack);
        }

        public int GetSignalCount()
        {
            int count = 0;
            foreach (var signal in m_signals)
            {
                if (signal != null) count++;
            }

            return count;
        }

        public void Update(float deltaTime, float currentTime)
        {
            if (!m_isOn) return;

            for (int i = 0; i < m_signals.Count; i++)
            {
                var signalPack = m_signals[i];
                if (signalPack == null) continue;
                signalPack.timer += deltaTime;
                

                while (signalPack.timer >= Constants.SIGNAL_MOVING_DURATION)
                {
                    signalPack.blackBoard.time = new Time(currentTime);
                    signalPack.timer -= Constants.SIGNAL_MOVING_DURATION;
                    MoveSignal(signalPack.signal, signalPack.blackBoard, out signalPack.isDead);
                }

                if (signalPack.isDead) RemoveSignal(i);
                 
            }

            if (m_isTest && GetSignalCount() == 0)
            {
                m_onNoSignal.Invoke();
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
            
            //Debug.Log(signal);
            
        }
        
        
    }
}