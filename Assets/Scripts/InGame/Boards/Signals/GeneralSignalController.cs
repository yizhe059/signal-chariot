using System;
using System.Collections.Generic;
using InGame.Views;
using SetUps;

namespace InGame.Boards.Signals
{
    public class GeneralSignalController
    {
        private Dictionary<SignalType, SignalController> m_controllers = new();
        private List<SignalType> m_executeOrder = new();
        public GeneralSignalController(Board board, BoardView boardView)
        {
            foreach (SignalType type in Enum.GetValues(typeof(SignalType)))
            {
                if (type == SignalType.None) continue;
                
                m_controllers.Add(type, SignalController.CreateSignalController(board, boardView));
                if (type == SignalType.Normal) continue;
                m_executeOrder.Add(type);
            }
            
            m_executeOrder.Add(SignalType.Normal);

        }

        public void Update(float deltaTime, float currentTime)
        {
            foreach (var type in m_executeOrder)
            {
                m_controllers[type].Update(deltaTime, currentTime);
            }
        }
        
        public void CreateSignal(SignalSetUp setUp, int delay = 0, SignalType type = SignalType.Normal)
        {
            setUp.type = type;
            m_controllers[type].CreateSignal(setUp, delay);
        }

               
        public void Reset()
        {
            foreach (var pair in m_controllers)
            {
                pair.Value.Reset();
            }
        }

        public void Start()
        {
            foreach (var pair in m_controllers)
            {
                pair.Value.Start();
            }
        }
        
        public void Stop()
        {
            foreach (var pair in m_controllers)
            {
                pair.Value.Stop();
            }
        }
        

        public void TestStart()
        {
            foreach (var pair in m_controllers)
            {
                pair.Value.TestStart();
            }
        }
        

        public void TestStop()
        {
            foreach (var pair in m_controllers)
            {
                pair.Value.TestStop();
            }
        }

    }
}