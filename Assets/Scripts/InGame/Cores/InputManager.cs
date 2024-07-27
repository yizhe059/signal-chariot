using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace InGame.Cores
{
    public class InputManager
    {
        private PlayerInput m_playerInput;

        private UnityEvent<Vector2> m_onClicked = new UnityEvent<Vector2>();
        private UnityEvent<Vector2> m_onMouseMove = new UnityEvent<Vector2>();
        private UnityEvent m_onRotatePressed = new UnityEvent();

        public InputManager(PlayerInput input)
        {
            m_playerInput = input;
            m_playerInput.actions["Click"].started += OnClicked;
            m_playerInput.actions["MousePos"].performed += OnMouseMove;
            m_playerInput.actions["Rotate"].started += OnRotate;
        }
        

        public void RegisterClickEvent(UnityAction<Vector2> act)
        {
            m_onClicked.AddListener(act);
        }
        
        public void UnregisterClickEvent(UnityAction<Vector2> act)
        {
            m_onClicked.RemoveListener(act);
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
        
        private void OnClicked(InputAction.CallbackContext context)
        {
            var mousePosition = context.ReadValue<Vector2>();
            if (Camera.main == null)
            {
                Debug.LogError("No camera!");
                return;
            }
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            //Debug.Log(worldPosition);

            m_onClicked.Invoke(worldPosition);
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
            //Debug.Log(worldPosition);

            m_onMouseMove.Invoke(worldPosition);
        }
        
        private void OnRotate(InputAction.CallbackContext context)
        {

            m_onRotatePressed.Invoke();
        }
    }
}