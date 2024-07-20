using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace World.Cores
{
    public class InputManager
    {
        private PlayerInput m_playerInput;

        private UnityEvent<Vector2> m_onClicked = new UnityEvent<Vector2>();

        public InputManager(PlayerInput input)
        {
            m_playerInput = input;
            m_playerInput.actions["Click"].started += OnClicked;
        }
        

        public void RegisterClickEvent(UnityAction<Vector2> act)
        {
            m_onClicked.AddListener(act);
        }
        
        public void UnregisterClickEvent(UnityAction<Vector2> act)
        {
            m_onClicked.RemoveListener(act);
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
        
        
    }
}