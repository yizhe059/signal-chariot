using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace InGame.Cores
{
    public class InputManager
    {
        private PlayerInput m_playerInput;

        private UnityEvent<Vector2> m_onMouseLeftClicked = new();
        private UnityEvent<Vector2> m_onMouseMove = new();
        private UnityEvent m_onRotatePressed = new();
        private UnityEvent<Vector2> m_onMoveKeyPressed = new();
        private UnityEvent<Vector2> m_onMoveKeyReleased = new();

        public InputManager(PlayerInput input)
        {
            m_playerInput = input;
            m_playerInput.actions["Click"].started += OnClicked;
            m_playerInput.actions["MousePos"].performed += OnMouseMove;
            m_playerInput.actions["Rotate"].started += OnRotate;

            m_playerInput.actions["Move"].performed += OnMoveKeyPressed;
            m_playerInput.actions["Move"].canceled += OnMoveKeyReleased;
        }

        #region Event Listeners
        public void RegisterClickEvent(UnityAction<Vector2> act)
        {
            m_onMouseLeftClicked.AddListener(act);
        }
        
        public void UnregisterClickEvent(UnityAction<Vector2> act)
        {
            m_onMouseLeftClicked.RemoveListener(act);
        }
        
        public void RegisterMouseMoveEvent(UnityAction<Vector2> act)
        {
            m_onMouseMove.AddListener(act);
        }
        
        public void UnregisterMouseMoveEvent(UnityAction<Vector2> act)
        {
            m_onMouseMove.RemoveListener(act);
        }

        public void RegisterRotateEvent(UnityAction act)
        {
            m_onRotatePressed.AddListener(act);
        }
        
        public void UnregisterRotateEvent(UnityAction act)
        {
            m_onRotatePressed.RemoveListener(act);
        }

        public void RegisterMoveEvent(UnityAction<Vector2> act)
        {
            m_onMoveKeyPressed.AddListener(act);
            m_onMoveKeyReleased.AddListener(act);
        }

        public void UnregisterMoveEvent(UnityAction<Vector2> act)
        {
            m_onMoveKeyPressed.RemoveListener(act);
            m_onMoveKeyReleased.RemoveListener(act);
        }
        #endregion
        
        #region Events
        private void OnClicked(InputAction.CallbackContext context)
        {
            var mousePosition = context.ReadValue<Vector2>();
            if (Camera.main == null)
            {
                Debug.LogError("No camera!");
                return;
            }
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            m_onMouseLeftClicked.Invoke(worldPosition);
        }
        
        private void OnMouseMove(InputAction.CallbackContext context)
        {
            var mousePosition = context.ReadValue<Vector2>();
            if (Camera.main == null)
            {
                Debug.LogError("No camera!");
                return;
            }
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            m_onMouseMove.Invoke(worldPosition);
        }
        
        private void OnRotate(InputAction.CallbackContext context)
        {
            m_onRotatePressed.Invoke();
        }

        private void OnMoveKeyPressed(InputAction.CallbackContext context)
        {
            var inputDirection = context.ReadValue<Vector2>();
            m_onMoveKeyPressed.Invoke(inputDirection);
        }

        private void OnMoveKeyReleased(InputAction.CallbackContext context)
        {
            m_onMoveKeyReleased.Invoke(Vector2.zero);
        }
        #endregion
    }
}