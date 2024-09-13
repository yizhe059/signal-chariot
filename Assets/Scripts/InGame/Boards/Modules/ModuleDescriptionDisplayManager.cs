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
            BoardConsoleUI boardConsoleUI = UIManager.Instance.GetUI(UIElements.BoardConsole) as BoardConsoleUI;
            string[] content = new string[5]{
                module.name,
                module.category.ToString(),
                module.desc,
                module.tag,
                module.function
            };
            if (boardConsoleUI != null) boardConsoleUI.DisplayModuleInfo(content);
        }

        public void UndisplayModule(Module module)
        {
            if (!m_isOn) return;
            BoardConsoleUI boardConsoleUI = UIManager.Instance.GetUI(UIElements.
            BoardConsole) as BoardConsoleUI;
            if (boardConsoleUI != null) boardConsoleUI.ClearModuleInfo();
        }
    }
}