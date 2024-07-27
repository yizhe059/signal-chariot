using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Utils.Common;
using World.InGameStates;
using World.Modules;
using World.Signals;
using World.Views;

namespace World.Cores
{
    public class GameManager: MonoSingleton<GameManager>
    {
        [SerializeField]
        private PlayerInput m_playerInput;
        
        private InputManager m_inputManager;

        private ModuleLib m_moduleLib;

        private SignalController m_signalController;
        
        [SerializeField] private SetUp m_setUp;
        private Board m_board;

        [SerializeField]
        private BoardView m_boardView;

        protected override void Init()
        {
            m_inputManager = new InputManager(m_playerInput);

            m_moduleLib = new ModuleLib();
            m_moduleLib.Init(m_setUp.moduleLibrary);

            m_board = new Board(m_setUp.boardSetUp);
            m_boardView.Init(m_board, m_setUp.boardSetUp);
            
            m_signalController = SignalController.CreateSignalController(m_board, m_boardView);

            ChangeToBoardWaitingState();
            
            m_signalController.CreateSignal(new SignalSetUp
            {
                dir = Signal.Direction.Right,
                energy = 3,
                pos = new BoardPosition(2,2)
            });
            
            m_signalController.Reset();
            m_signalController.Start();
            //Debug.Log(m_board);
        }

        public InputManager GetInputManager() => m_inputManager;
        public ModuleLib GetModuleLib() =>  m_moduleLib;
        public SignalController GetSignalController() => m_signalController;

        public void Update()
        {
            m_signalController.Update(UnityEngine.Time.deltaTime);
        }

        public void ChangeToBoardWaitingState()
        {
            WorldState.instance.nextState = BoardWaitingState.CreateState(m_board, m_boardView);
        }
        
        public void ChangeToAddSlotState()
        {
            WorldState.instance.nextState = AddSlotState.CreateAddSlotState(m_boardView, m_board, Int32.MaxValue);
        }

        public void ChangeToModulePlacingState(Module module)
        {
            WorldState.instance.nextState = ModulePlacingState.CreateState(m_board, m_boardView, module);
        }

        public void ChangeToNullState()
        {
            WorldState.instance.nextState = null;
        }
    }
}