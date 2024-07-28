using System;

using UnityEngine;
using UnityEngine.InputSystem;

using InGame.Boards;
using InGame.Boards.Modules;
using InGame.Boards.Signals;
using InGame.BattleFields.Chariots;
using InGame.InGameStates;
using InGame.Views;

using SetUps;
using Utils.Common;

namespace InGame.Cores
{
    public class GameManager: MonoSingleton<GameManager>
    {   
        [SerializeField] private PlayerInput m_playerInput;
        [SerializeField] private SetUp m_setUp;

        [Header("Managers")]
        private InputManager m_inputManager;
        private TimeEffectManager m_timeEffectManager;

        [Header("Chariot")]
        private Chariot m_chariot;
        private ChariotView m_chariotView;

        [Header("Board")]
        private Board m_board;
        private BoardView m_boardView;
        private ModuleLib m_moduleLib;
        private SignalController m_signalController;

        protected override void Init()
        {
            m_inputManager = new InputManager(m_playerInput);
            m_timeEffectManager = TimeEffectManager.CreateTimeEffectManager();

            InitChariot();
            InitBoard();

            ChangeToBoardWaitingState(); // initial state is board preparation
        }

        private void InitChariot()
        {
            m_chariot = new Chariot(m_setUp.chariotSetUp);
            
            GameObject chariotPref = Resources.Load<GameObject>("Prefabs/3C/ChariotView");
            GameObject chariotGO = Instantiate(chariotPref);
            m_chariotView = chariotGO.GetComponent<ChariotView>();
            m_chariotView.Init(m_chariot);
        }

        private void InitBoard()
        {
            m_moduleLib = new ModuleLib();
            m_moduleLib.Init(m_setUp.moduleLibrary);

            m_board = new Board(m_setUp.boardSetUp);
            
            GameObject boardPref = Resources.Load<GameObject>("Prefabs/Board/BoardView");
            GameObject boardGO = Instantiate(boardPref);
            m_boardView = boardGO.GetComponent<BoardView>();
            m_boardView.Init(m_board, m_setUp.boardSetUp);
            
            m_signalController = SignalController.CreateSignalController(m_board, m_boardView);
        }

        public void Update()
        {
            m_signalController?.Update(UnityEngine.Time.deltaTime, UnityEngine.Time.time);
            m_timeEffectManager?.Update(UnityEngine.Time.deltaTime, UnityEngine.Time.time);
        }

        #region Getters
        public InputManager GetInputManager() => m_inputManager;
        public ModuleLib GetModuleLib() =>  m_moduleLib;
        public SignalController GetSignalController() => m_signalController;
        public TimeEffectManager GetTimeEffectManager() => m_timeEffectManager;
        public Board GetBoard() => m_board;
        public Chariot GetChariot() => m_chariot;
        public InGameStateType GetCurrentInGameState() => WorldState.instance.currentState.type;
        #endregion

        #region World State Machine
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
            WorldState.instance.nextState = BoardTestState.CreateState(m_timeEffectManager, m_signalController);
        }

        public void ChangeToBattleState()
        {
            WorldState.instance.nextState = BattleState.CreateState(m_chariot, m_chariotView);
        }
        
        public void ChangeToBattleResultState()
        {
            WorldState.instance.nextState = BattleResultState.CreateState();
        }

        public void ChangeToNullState()
        {
            WorldState.instance.nextState = null;
        }
        #endregion
    }
}