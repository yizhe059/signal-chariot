using UnityEngine;
using UnityEngine.UIElements;

using Utils.Common;

namespace InGame.UI
{
    public class TutorialUI : MonoBehaviour, IHidable
    {
        [SerializeField] private UIDocument m_doc;
        private Button m_close;

        private void Awake()
        {
            m_close = m_doc.rootVisualElement.Q<Button>("close");
            m_close.clicked += () => {
                int bitmask = UIManager.Instance.GetDisplayBit(
                    UIElements.Tutorial
                );
                UIManager.Instance.RemoveDisplayUI(bitmask);
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