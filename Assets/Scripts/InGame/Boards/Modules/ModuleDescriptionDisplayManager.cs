using InGame.Cores;
using UnityEngine;

namespace InGame.Boards.Modules
{
    public class ModuleDescriptionDisplayManager
    {
        private InputManager m_inputManager;
        private bool m_isOn;
        private Vector2 m_currentScreenPos;
        
        public ModuleDescriptionDisplayManager(InputManager inputManager)
        {
            m_inputManager = inputManager;
        }

        private void OnMouseMove(Vector2 pos)
        {
            m_currentScreenPos = pos;
        }

        public void Start()
        {
            m_isOn = true;
            m_inputManager.RegisterScreenMouseMoveEvent(OnMouseMove);
        }

        public void Stop()
        {
            m_isOn = false;
            m_inputManager.UnregisterScreenMouseMoveEvent(OnMouseMove);
        }

        public void DisplayModule(Module module)
        {
            if (!m_isOn) return;
            Debug.Log($"Display {module.name} on {m_currentScreenPos}");
            ModuleCardUI.Instance.Show();
            ModuleCardUI.Instance.SetContent(module.name, m_currentScreenPos);
        }

        public void UndisplayModule(Module module)
        {
            if (!m_isOn) return;
            Debug.Log($"Undisplay {module.name}");
            ModuleCardUI.Instance.Hide();
        }
        
        #region UI



        #endregion
    }
}