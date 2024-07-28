using System;

using UnityEngine;
using UnityEngine.InputSystem;

using Chariots;
using InGame.Boards;
using InGame.Boards.Modules;
using InGame.Boards.Signals;
using InGame.InGameStates;
using InGame.Views;
using SetUps;
using Utils.Common;

namespace InGame.Cores
{
    public class GameManager: MonoSingleton<GameManager>
    {        
        private Chariot m_chariot;
        private InputManager m_inputManager;

        private Board m_board;
        private ModuleLib m_moduleLib;
        private SignalController m_signalController;
        private TimeEffectManager m_timeEffectManager;

        [SerializeField] private PlayerInput m_playerInput;
        [SerializeField] private SetUp m_setUp;
        [SerializeField] private BoardView m_boardView;

        protected override void Init()
        {
            m_chariot = new Chariot(m_setUp.chariotSetUp);
            
            m_inputManager = new InputManager(m_playerInput);

            m_timeEffectManager = TimeEffectManager.CreateTimeEffectManager();
            
            m_moduleLib = new ModuleLib();
            m_moduleLib.Init(m_setUp.moduleLibrary);

            m_board = new Board(m_setUp.boardSetUp);
            m_boardView.Init(m_board, m_setUp.boardSetUp);
            m_signalController = SignalController.CreateSignalController(m_board, m_boardView);
        
            ChangeToBoardWaitingState(); // initial state is board preparation
        }

        public InputManager GetInputManager() => m_inputManager;
        public ModuleLib GetModuleLib() =>  m_moduleLib;
        public SignalController GetSignalController() => m_signalController;
        public TimeEffectManager GetTimeEffectManager() => m_timeEffectManager;

        public Board GetBoard() => m_board;
        
        public void Update()
        {
            m_signalController?.Update(UnityEngine.Time.deltaTime, UnityEngine.Time.time);
            m_timeEffectManager?.Update(UnityEngine.Time.deltaTime, UnityEngine.Time.time);
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

        public void ChangeToBoardBattleState()
        {
            WorldState.instance.nextState = BoardBattleState.CreateState(m_timeEffectManager, m_signalController);
        }

        public void ChangeToBattleState()
        {
            WorldState.instance.nextState = BattleState.CreateState();
        }

        public void ChangeToNullState()
        {
            WorldState.instance.nextState = null;
        }

        public InGameStateType GetCurrentInGameState()
        {
            return WorldState.instance.currentState.type;
        }
    }
}