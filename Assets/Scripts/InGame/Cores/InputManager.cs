﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace InGame.Cores
{
    public class InputManager
    {
        private PlayerInput m_playerInput;

        private Dictionary<Camera, UnityEvent<Vector2>> m_onMouseLeftClickedEvents = new();
        private Dictionary<Camera, UnityEvent<Vector2>> m_onMouseMoveEvents = new();
        private UnityEvent<Vector2> m_onMouseMoveScreenPos = new();
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

        public void Clear()
        {
            m_playerInput.actions["Click"].started -= OnClicked;
            m_playerInput.actions["MousePos"].performed -= OnMouseMove;
            m_playerInput.actions["Rotate"].started -= OnRotate;
            m_playerInput.actions["Move"].performed -= OnMoveKeyPressed;
            m_playerInput.actions["Move"].canceled -= OnMoveKeyReleased;
        }

        #region Event Listeners
        public void RegisterClickEvent(Camera camera, UnityAction<Vector2> act)
        {
            if (!m_onMouseLeftClickedEvents.TryGetValue(camera, out var evt))
            {
                evt = new UnityEvent<Vector2>();
                m_onMouseLeftClickedEvents.Add(camera, evt);
            }
            evt.AddListener(act);
        }
        
        public void UnregisterClickEvent(Camera camera, UnityAction<Vector2> act)
        {
            if (!m_onMouseLeftClickedEvents.TryGetValue(camera, out var evt))
            {
                Debug.LogError("No this type of camera");
                return;
            }
            evt.RemoveListener(act);
        }
        
        public void RegisterMouseMoveEvent(Camera camera, UnityAction<Vector2> act)
        {
            if (!m_onMouseMoveEvents.TryGetValue(camera, out var evt))
            {
                evt = new UnityEvent<Vector2>();
                m_onMouseMoveEvents.Add(camera, evt);
            }
            evt.AddListener(act);
        }
        
        public void UnregisterMouseMoveEvent(Camera camera, UnityAction<Vector2> act)
        {
            if (!m_onMouseMoveEvents.TryGetValue(camera, out var evt))
            {
                Debug.LogError("No this type of camera");
                return;
            }
            evt.RemoveListener(act);
        }
        
        public void RegisterScreenMouseMoveEvent(UnityAction<Vector2> act)
        {
            if (act == null) return;
            m_onMouseMoveScreenPos.AddListener(act);
        }
        
        public void UnregisterScreenMouseMoveEvent(UnityAction<Vector2> act)
        {
            if (act == null) return;
            m_onMouseMoveScreenPos.RemoveListener(act);
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
            
            foreach (var tuple in m_onMouseLeftClickedEvents)
            {
                var camera = tuple.Key;
                var evt = tuple.Value;
                
                Vector2 worldPosition = camera.ScreenToWorldPoint(mousePosition);
                evt.Invoke(worldPosition);
            }
        }
        
        private void OnMouseMove(InputAction.CallbackContext context)
        {
            var mousePosition = context.ReadValue<Vector2>();
            m_onMouseMoveScreenPos.Invoke(mousePosition);
            
            foreach (var tuple in m_onMouseMoveEvents)
            {
                var camera = tuple.Key;
                var evt = tuple.Value;
                
                Vector2 worldPosition = camera.ScreenToWorldPoint(mousePosition);
                evt.Invoke(worldPosition);
            }
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