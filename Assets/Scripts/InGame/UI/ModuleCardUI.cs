using UnityEngine;
using UnityEngine.UIElements;

using Utils.Common;

namespace InGame.UI
{
    public class ModuleCardUI : MonoBehaviour, IHidable
    {
        [SerializeField] private UIDocument m_doc;
        [SerializeField] private float X_DISPLAY_THRESHOLD = 0.2f;
        [SerializeField] private float Y_DISPLAY_THRESHOLD = 0.3f;
        private VisualElement m_root;
        private VisualElement m_panel;
        private Label m_name;
        private Label m_description;

        private void Awake()
        {
            m_root = m_doc.rootVisualElement;

            m_panel = m_root.Q("panel");

            m_name = m_root.Q<Label>("name");
            m_description = m_root.Q<Label>("description");
        }

        public void SetContent(string name, string description, Vector2 position)
        {
            m_name.text = name;
            m_description.text = description;

            // Vector2 mousePosition = position;
            Vector2 panelPosition = m_root.WorldToLocal(position);

            Vector2 screenSize = new(Screen.width, Screen.height);
            panelPosition.y = screenSize.y - panelPosition.y;

            // right side out of screen
            if(panelPosition.x > screenSize.x * (1f - X_DISPLAY_THRESHOLD)) 
                panelPosition.x -= screenSize.x * X_DISPLAY_THRESHOLD;
            // left side out of screen
            if(panelPosition.x < 0)
                panelPosition.x = 0;
            // top side out of screen
            if(panelPosition.y > screenSize.y *  (1f - Y_DISPLAY_THRESHOLD)) 
                panelPosition.y -= screenSize.y * Y_DISPLAY_THRESHOLD;
            // bottom side out of screen
            if(panelPosition.y < screenSize.y * Y_DISPLAY_THRESHOLD)
                panelPosition.y = screenSize.y * Y_DISPLAY_THRESHOLD;

            m_panel.style.left = new Length(panelPosition.x, LengthUnit.Pixel);
            m_panel.style.top = new Length(panelPosition.y, LengthUnit.Pixel);

            Debug.Log("mouse position " + position.x + ", " + position.y);
            Debug.Log("panel position " + panelPosition.x + ", " + panelPosition.y);
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