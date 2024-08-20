using UnityEngine;
using UnityEngine.UIElements;

using Utils.Common;
using InGame.Cores;

namespace InGame.UI
{
    public class NavigationBarUI : MonoBehaviour, IHidable
    {
        [SerializeField] private UIDocument m_doc;
        private VisualElement m_root;
        private Button m_exitButton;
        private Button m_boardButton;
        private Button m_manufactureButton;
        private Button m_marchButton;
        
        private void Awake()
        {
            m_root = m_doc.rootVisualElement;
            Register();
        }

        private void Register()
        {
            m_exitButton = m_root.Q<Button>("exit");
            m_boardButton = m_root.Q<Button>("board");
            m_manufactureButton = m_root.Q<Button>("manufacture");
            m_marchButton = m_root.Q<Button>("march");

            m_exitButton.clicked += () => {
                GameManager.Instance.ChangeToInitState();
            };

            m_boardButton.clicked += () => {
                GameManager.Instance.ChangeToBoardWaitingState();
            };

            m_manufactureButton.clicked += () => {
                // TODO
            };

            m_marchButton.clicked += () => {
                GameManager.Instance.ChangeToBattleState();
            };
        }

        public void Hide()
        {
            m_doc.rootVisualElement.style.display = DisplayStyle.None;
        }
        public void Show()
        {
            m_doc.rootVisualElement.style.display = DisplayStyle.Flex;
        }
    }
}