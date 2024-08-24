using InGame.Cores;
using InGame.UI;

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
            int bitmask = UIManager.Instance.GetDisplayBit(
                UIElements.ModuleInfoCard
            );
            UIManager.Instance.AddDisplayUI(bitmask);

            ModuleCardUI moduleCardUI = UIManager.Instance.GetUI(UIElements.ModuleInfoCard) as ModuleCardUI;
            if (moduleCardUI != null)
                moduleCardUI.SetContent(module.name, module.desc, module.category, m_currentScreenPos);
        }

        public void UndisplayModule(Module module)
        {
            if (!m_isOn) return;
            int bitmask = UIManager.Instance.GetDisplayBit(
                UIElements.ModuleInfoCard
            );
            UIManager.Instance.RemoveDisplayUI(bitmask);
        }
    }
}