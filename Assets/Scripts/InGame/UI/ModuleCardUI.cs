using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

using Utils.Common;

public class ModuleCardUI : MonoSingleton<ModuleCardUI>, IHidable
{
    [SerializeField] private UIDocument m_doc;
    [SerializeField] private int X_DISPLAY_OFFSET = 150;
    [SerializeField] private int Y_DISPLAY_OFFSET = 150;
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

    public void SetContent(string name, string description, Vector2 positon)
    {
        
        m_name.text = name;
        m_description.text = description;
        m_panel.style.left = new Length(positon.x - X_DISPLAY_OFFSET, LengthUnit.Pixel);
        m_panel.style.top = new Length(Screen.height - positon.y - Y_DISPLAY_OFFSET, LengthUnit.Pixel);
        // TODO: if ui out of bound, then display in another way
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
