using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Utils.Common;
using World.InGameStates;
using World.Modules;
using World.Views;

namespace World.Cores
{
    public class GameManager: MonoSingleton<GameManager>
    {
        [SerializeField]
        private PlayerInput m_playerInput;
        
        private InputManager m_inputManager;

        private ModuleLib m_moduleLib;
        
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
            Debug.Log(m_board);
            m_boardView.Init(m_board, m_setUp.boardSetUp);

            ChangeToBoardWaitingState();
            //Debug.Log(m_board);
        }

        public InputManager GetInputManager() => m_inputManager;
        public ModuleLib GetModuleLib() =>  m_moduleLib;

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