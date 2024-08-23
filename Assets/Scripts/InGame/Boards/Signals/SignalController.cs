using System.Collections.Generic;
using InGame.Effects;
using InGame.Views;
using SetUps;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Utils.Common;

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

        private class SetUpPack
        {
            public SignalSetUp setUp;
            public int delay;
            public bool isEmpty;
        }

        private float m_timer;
        private List<SignalPack> m_signals = new List<SignalPack>();
        private List<SetUpPack> m_setUpQueues = new List<SetUpPack>();
        private Grid<List<int>> m_signalDistributions;
        
        private bool m_isOn = false;
        private bool m_isTest = false;
        private bool m_halfCheck = false;
        
        private Board m_board;
        private BoardView m_boardView;

        public static SignalController CreateSignalController(Board board, BoardView boardView)
        {
            return new SignalController
            {
                m_board = board, 
                m_boardView = boardView,
                m_signalDistributions = new Grid<List<int>>(board.width, board.height, 1f, Vector3.zero,
                    ((grid, x, y) => new List<int>()))
            }; 
        }

        public void CreateSignal(SignalSetUp setUp, int delay = 0)
        {
            
            for (int i = 0; i < m_setUpQueues.Count; i++)
            {
                if (!m_setUpQueues[i].isEmpty) continue;
                m_setUpQueues[i].setUp = setUp;
                m_setUpQueues[i].delay = delay;
                m_setUpQueues[i].isEmpty = false;
                return;
            }
            
            var newPack = new SetUpPack
            {
                setUp = setUp,
                delay = delay,
                isEmpty = false
            };
            
            m_setUpQueues.Add(newPack);
        }

        private int AddSignal(SignalSetUp setUp)
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
            m_signalDistributions.GetValue(setUp.pos.x, setUp.pos.y).Add(id);
            if (m_isOn) signal.Start();
            else signal.Stop();

            return id;
        }
        
        private void RemoveSignal(int id)
        {
            var signalPack = m_signals[id];
            if (signalPack == null) return;
            var pos = signalPack.signal.pos;
            m_signalDistributions.GetValue(pos.x, pos.y).Remove(id);
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
            m_timer = Constants.SIGNAL_MOVING_DURATION;
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
        

        public void TestStart()
        {
            m_isOn = true;
            m_isTest = true;
            m_timer = Constants.SIGNAL_MOVING_DURATION;
            foreach (var signalPack in m_signals)
            {
                if (signalPack == null) continue;
                signalPack.blackBoard.isTest = m_isTest;
                signalPack.signal.Start();
            }
        }
        

        public void TestStop()
        {
            m_isOn = false;
            m_isTest = false;
            
            for (int i = 0; i < m_signals.Count; i++)
            {
                RemoveSignal(i);
            }
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

            m_timer += deltaTime;

            if (m_timer >= Constants.SIGNAL_MOVING_DURATION / 2 && !m_halfCheck)
            {
                m_halfCheck = true;
                FuseBorderSignals();
                RemoveSignals();
                
            }

            while (m_timer >= Constants.SIGNAL_MOVING_DURATION)
            {
                if (m_timer >= Constants.SIGNAL_MOVING_DURATION / 2 && !m_halfCheck)
                {
                    m_halfCheck = true;
                    FuseBorderSignals();
                    RemoveSignals();

                }
                
                m_timer -= Constants.SIGNAL_MOVING_DURATION;
                MoveSignals();
                FuseSignals();
                TriggerSignals(deltaTime, currentTime);
                RemoveSignals();
                AddSignals();
                m_halfCheck = false;
            }
        }

        private void MoveSignals()
        {
            for (int i = 0; i < m_signals.Count; i++)
            {
                var signalPack = m_signals[i];
                if (signalPack == null) continue;
                
                MoveSignal(signalPack.signal, out signalPack.isDead);
                
            }
        }

        private void FuseSignals()
        {
            for (int x = 0; x < m_signalDistributions.width; x++)
            {
                for (int y = 0; y < m_signalDistributions.height; y++)
                {
                    var list = m_signalDistributions.GetValue(x, y);
                    FuseSignalList(list, new BoardPosition(x,y));
                }
            }
        }

        private void FuseSignalList(List<int> list, BoardPosition pos)
        {
            List<int> up = new List<int>(), down = new List<int>(), left = new List<int>(), right = new List<int>();

            for (int idx = list.Count - 1; idx >= 0; idx--)
            {
                var id = list[idx];
                var signal = m_signals[id].signal;
                
                switch (signal.dir)
                {
                    case Signal.Direction.Up:
                        up.Add(id);
                        break;
                    case Signal.Direction.Down:
                        down.Add(id);
                        break;
                    case Signal.Direction.Left:
                        left.Add(id);
                        break;
                    case Signal.Direction.Right:
                        right.Add(id);
                        break;
                    default:
                        up.Add(id);
                        break;
                }
            }

            

            if (up.Count + down.Count > 1)
            {
                int upEnergy = 0;
                foreach (var id in up)
                {
                    upEnergy += m_signals[id].signal.energy;
                    m_signals[id].isDead = true;
                }

                foreach (var id in down)
                {
                    upEnergy -= m_signals[id].signal.energy;
                    m_signals[id].isDead = true;
                }

                if (upEnergy != 0)
                {
                    var newID = AddSignal(new SignalSetUp
                    {
                        dir = upEnergy > 0? Signal.Direction.Up: Signal.Direction.Down,
                        energy = upEnergy > 0? upEnergy: -upEnergy,
                        pos = pos
                    });
                    
                }
                
            }
            
            if (right.Count + left.Count > 1)
            {
                int rightEnergy = 0;
                foreach (var id in right)
                {
                    rightEnergy += m_signals[id].signal.energy;
                    m_signals[id].isDead = true;
                }

                foreach (var id in left)
                {
                    rightEnergy -= m_signals[id].signal.energy;
                    m_signals[id].isDead = true;
                }
                
                if (rightEnergy != 0)
                {
                    var newID = AddSignal(new SignalSetUp
                    {
                        dir = rightEnergy > 0? Signal.Direction.Right: Signal.Direction.Left,
                        energy = rightEnergy > 0? rightEnergy: -rightEnergy,
                        pos = pos
                    });
                    
                }
            }
            

            
        }

        private void FuseBorderSignals()
        {
            for (int x = 0; x < m_signalDistributions.width - 1; x++)
            {
                for (int y = 0; y < m_signalDistributions.height - 1; y++)
                {
                    var pos = new BoardPosition(x, y);
                    FuseBorderSignalList(pos, Signal.Direction.Right);
                    FuseBorderSignalList(pos, Signal.Direction.Up);
                }
            }
        }

        private void FuseBorderSignalList(BoardPosition first, Signal.Direction dir)
        {
            BoardPosition second;
            Signal.Direction reversedDir;
            if (dir == Signal.Direction.Left)
            {
                dir = Signal.Direction.Right;
                second = first;
                first.x -= 1;
                reversedDir = Signal.Direction.Left;
            }else if (dir == Signal.Direction.Right)
            {
                second = first;
                second.x += 1;
                reversedDir = Signal.Direction.Left;
            }else if (dir == Signal.Direction.Up)
            {
                second = first;
                second.y += 1;
                reversedDir = Signal.Direction.Down;
            }
            else
            {
                dir = Signal.Direction.Up;
                second = first;
                first.y -= 1;
                reversedDir = Signal.Direction.Down;
            }

            var firstList = m_signalDistributions.GetValue(first.x, first.y);
            var secondList = m_signalDistributions.GetValue(second.x, second.y);

            var dirList = new List<int>();
            var reverseDirList = new List<int>();
            
            // To DO: can be put into a function becasue fuse signals function also use this structure
            foreach (var id in firstList)
            {
                var signalDir = m_signals[id].signal.dir;
                if (signalDir == dir) dirList.Add(id);
            }

            foreach (var id in secondList)
            {
                var signalDir = m_signals[id].signal.dir;
                if (signalDir == reversedDir) reverseDirList.Add(id);
            }

            if (dirList.Count + reverseDirList.Count > 1)
            {
                int dirEnergy = 0;

                foreach (var id in dirList)
                {
                    dirEnergy += m_signals[id].signal.energy;
                    m_signals[id].isDead = true;
                }
                
                foreach (var id in reverseDirList)
                {
                    dirEnergy -= m_signals[id].signal.energy;
                    m_signals[id].isDead = true;
                }

                if (dirEnergy != 0)
                {
                    var newPos = dirEnergy > 0 ? first : second;
                    var newID = AddSignal(new SignalSetUp
                    {
                        dir = dirEnergy > 0? dir: reversedDir,
                        energy = dirEnergy > 0? dirEnergy: -dirEnergy,
                        pos = newPos
                    });
                    
                }
            }
        }
        
        private void TriggerSignals(float deltaTime, float currentTime)
        {
            for (int i = 0; i < m_signals.Count; i++)
            {
                var signalPack = m_signals[i];
                if (signalPack == null || signalPack.isDead) continue;
                signalPack.blackBoard.time = new Time(currentTime);
                TriggerSignal(signalPack.signal, signalPack.blackBoard, out signalPack.isDead);
            }
        }

        private void RemoveSignals()
        {
            for (int i = 0; i < m_signals.Count; i++)
            {
                var signalPack = m_signals[i];
                if (signalPack == null) continue;
                
                if (signalPack.isDead) RemoveSignal(i);
            }
        }

        private void AddSignals()
        {
            foreach (var pack in m_setUpQueues)
            {
                if (pack.isEmpty) continue;
                
                if (pack.delay <= 0)
                {
                    AddSignal(pack.setUp);
                    pack.isEmpty = true;
                }
                else
                {
                    pack.delay--;
                }
            }
        }
        private void MoveSignal(Signal signal, out bool isDead)
        {
            
            var pos = signal.pos;
            m_signalDistributions.GetValue(pos.x, pos.y).Remove(signal.id);
            
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
            m_signalDistributions.GetValue(newPos.x, newPos.y).Add(signal.id);
            isDead = false;
        }

        private void TriggerSignal(Signal signal, EffectBlackBoard bb, out bool isDead)
        {
            var pos = signal.pos;
            
            m_board.TriggerEffect(pos.x, pos.y, bb);

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