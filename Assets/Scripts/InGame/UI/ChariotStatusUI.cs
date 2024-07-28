using UnityEngine;
using UnityEngine.UIElements;

using Utils.Common;

namespace InGame.UI
{
    public class ChariotStatusUI : MonoSingleton<ChariotStatusUI>, IHidable
    {
        [SerializeField] private UIDocument m_doc;
        private VisualElement m_root;
        
        private void Awake()
        {
            m_root = m_doc.rootVisualElement;
        }

        public void Hide()
        {
            m_doc.rootVisualElement.style.display = DisplayStyle.None;
        }

        public void Show()
        {
            m_root.style.display = DisplayStyle.Flex;
        }
    }
}