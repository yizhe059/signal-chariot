using UnityEngine;
using UnityEngine.UIElements;

using Utils.Common;

namespace InGame.UI
{
    public class ModuleInfoUI : IHidable
    {
        private VisualElement m_root;
        private VisualElement m_panel;
        private Label m_name;
        private Label m_category;
        private Label m_energyConsumeNum;
        private Label m_energyConsumeType;
        private Label m_desc;
        private Label m_tag;
        private Label m_function;

        public ModuleInfoUI(VisualElement root)
        {
            m_root = root;
            m_panel = m_root.Q("panel");
            m_name = m_root.Q<Label>("name");
            m_category = m_root.Q<Label>("category");
            m_energyConsumeNum = m_root.Q<Label>("consumeNum");
            m_energyConsumeType = m_root.Q<Label>("consumeType");
            m_desc = m_root.Q<Label>("description");
            m_tag = m_root.Q<Label>("tag");
            m_function = m_root.Q<Label>("functionality");
            ClearInfo();
        }

        public void SetInfo(string[] content)
        {
            // TODO: set other info
            m_name.text = content[0];
            m_category.text = content[1];
            m_desc.text = content[2];
            m_tag.text = content[3];
            m_function.text = content[4];
            Show();
        }

        public void ClearInfo()
        {
            m_name.text = "";
            m_category.text = "";
            m_desc.text = "";
            m_tag.text = "";
            m_function.text = "";
            Hide();
        }

        public void Hide()
        {
            m_root.style.visibility = Visibility.Hidden;
        }

        public void Show()
        {
            m_root.style.visibility = Visibility.Visible;
        }
    }
}