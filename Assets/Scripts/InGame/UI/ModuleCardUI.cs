using InGame.Boards.Modules;
using UnityEngine;
using UnityEngine.UIElements;

using Utils.Common;

namespace InGame.UI
{
    public class ModuleCardUI : MonoBehaviour, IHidable
    {
        [SerializeField] private UIDocument m_doc;
        [SerializeField] private float X_DISPLAY_THRESHOLD = 0.3f;
        [SerializeField] private float Y_DISPLAY_THRESHOLD = 0.5f;
        private VisualElement m_root;
        private VisualElement m_panel;
        private Label m_name;
        private Label m_category;
        private Label m_desc;
        private Label m_tag;
        private Label m_function;

        private void Awake()
        {
            m_root = m_doc.rootVisualElement;

            m_panel = m_root.Q("panel");

            m_name = m_root.Q<Label>("name");
            m_category = m_root.Q<Label>("category");

            m_desc = m_root.Q<Label>("description");
            m_tag = m_root.Q<Label>("tag");
            m_function = m_root.Q<Label>("functionality");
        }

        public void SetContent(string[] content, Vector2 position)
        {
            m_name.text = content[0];
            m_category.text = content[1];
            m_desc.text = content[2];
            m_tag.text = content[3];
            m_function.text = content[4];

            Vector2 panelPosition = m_root.WorldToLocal(position);
            Vector2 screenSize = new(Screen.width, Screen.height);
            panelPosition.y = screenSize.y - panelPosition.y;
            panelPosition.x -= screenSize.x * X_DISPLAY_THRESHOLD;

            // right side out of screen
            if(panelPosition.x > screenSize.x * (1f - X_DISPLAY_THRESHOLD)) 
                panelPosition.x -= screenSize.x * X_DISPLAY_THRESHOLD;
            // left side out of screen
            if(panelPosition.x < 0)
                panelPosition.x = 0;
            // bottom side out of screen
            if(panelPosition.y > screenSize.y * (1f - Y_DISPLAY_THRESHOLD))
                panelPosition.y -= screenSize.y * Y_DISPLAY_THRESHOLD;
            // top side out of screen
            if(panelPosition.y < 0)
                panelPosition.y = screenSize.y * Y_DISPLAY_THRESHOLD;

            m_panel.style.left = new Length(panelPosition.x, LengthUnit.Pixel);
            m_panel.style.top = new Length(panelPosition.y, LengthUnit.Pixel);        
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