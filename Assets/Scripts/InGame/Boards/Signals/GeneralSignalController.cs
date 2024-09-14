using System;
using System.Collections.Generic;
using InGame.Views;
using SetUps;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace InGame.Boards.Signals
{
    public class GeneralSignalController
    {
        private Dictionary<SignalType, SignalController> m_controllers = new();
        private List<SignalType> m_executeOrder = new();

        private float m_timer;
        private bool m_halfCheck = false;
        private bool m_isOn = false;
        public bool isOn => m_isOn;
        public GeneralSignalController(Board board, BoardView boardView)
        {
            foreach (SignalType type in Enum.GetValues(typeof(SignalType)))
            {
                if (type == SignalType.None) continue;
                
                m_controllers.Add(type, SignalController.CreateSignalController(board, boardView, this));
                if (type == SignalType.Normal) continue;
                m_executeOrder.Add(type);
            }
            
            m_executeOrder.Add(SignalType.Normal);

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
                FuseOppositeDirSignals();
                TriggerSignals(deltaTime, currentTime);
                AddSignals();
                FuseOneDirSignals();
                RemoveSignals();
                m_halfCheck = false;
            }
        }
        
        #region Pipeline Functions

        private void ExecuteFunctions(UnityAction<SignalController> func)
        {
            foreach (var type in m_executeOrder)
            {
                func.Invoke(m_controllers[type]);
            }
        }

        private void FuseBorderSignals()
        {
            ExecuteFunctions(controller => controller.FuseBorderSignals());
        }

        private void RemoveSignals()
        {
            ExecuteFunctions(controller => controller.RemoveSignals());
        }

        private void MoveSignals()
        {
            ExecuteFunctions(controller => controller.MoveSignals());
        }

        private void FuseOppositeDirSignals()
        {
            ExecuteFunctions(controller => controller.FuseOppositeDirSignals());
        }

        private void TriggerSignals(float deltaTime, float currentTime)
        {
            ExecuteFunctions(controller => controller.TriggerSignals(deltaTime, currentTime));
        }

        private void AddSignals()
        {
            ExecuteFunctions(controller => controller.AddSignals());
        }

        private void FuseOneDirSignals()
        {
            ExecuteFunctions(controller => controller.FuseOneDirSignals());
        }
        
        
        #endregion
        
        public void CreateSignal(SignalSetUp setUp, int delay = 0, SignalType type = SignalType.Normal)
        {
            
            setUp.type = type;
            m_controllers[type].CreateSignal(setUp, delay);
        }

               
        public void Reset()
        {
            m_timer = Constants.SIGNAL_MOVING_DURATION;
            m_halfCheck = false;
            foreach (var pair in m_controllers)
            {
                pair.Value.Reset();
            }
        }

        public void Start()
        {
            m_isOn = true;
            m_timer = Constants.SIGNAL_MOVING_DURATION;
            m_halfCheck = false;
            foreach (var pair in m_controllers)
            {
                pair.Value.Start();
            }
        }
        
        public void Stop()
        {
            m_isOn = false;
            foreach (var pair in m_controllers)
            {
                pair.Value.Stop();
            }
        }
        

        public void TestStart()
        {
            m_isOn = true;
            m_timer = Constants.SIGNAL_MOVING_DURATION;
            foreach (var pair in m_controllers)
            {
                pair.Value.TestStart();
            }
        }
        

        public void TestStop()
        {
            m_isOn = false;
            foreach (var pair in m_controllers)
            {
                pair.Value.TestStop();
            }
        }

    }
}