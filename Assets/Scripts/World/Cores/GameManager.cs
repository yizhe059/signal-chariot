using UnityEngine;
using UnityEngine.InputSystem;
using Utils.Common;
using World.InGameStates;
using World.Views;

namespace World.Cores
{
    public class GameManager: MonoSingleton<GameManager>
    {
        [SerializeField]
        private PlayerInput m_playerInput;
        
        private InputManager m_inputManager;
        
        [SerializeField] private SetUp m_setUp;
        private Board m_board;

        [SerializeField]
        private BoardView m_boardView;

        protected override void Init()
        {
            m_inputManager = new InputManager(m_playerInput);
            m_board = new Board(m_setUp.boardSetUp);

            m_boardView.Init(m_board, m_setUp.boardSetUp);
            //Debug.Log(m_board);
        }

        public InputManager GetInputManager() => m_inputManager;


        public void ChangeToAddSlotState()
        {
            WorldState.instance.nextState = AddSlotState.CreateAddSlotState(m_boardView, m_board, 5);
        }

        public void ChangeToNullState()
        {
            WorldState.instance.nextState = null;
        }
    }
}