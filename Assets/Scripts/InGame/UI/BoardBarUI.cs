
using UnityEngine;
using UnityEngine.UIElements;

using InGame.Cores;
using InGame.UI;
using Utils.Common;
using InGame.InGameStates;

public class BoardBarUI : MonoSingleton<BoardBarUI>, IHidable
{
    [SerializeField] private UIDocument m_doc;
    private VisualElement m_root;
    private Button m_testButton;
    private Button m_addButton;

    private void Awake()
    {
        m_root = m_doc.rootVisualElement;
        Register();
    }

    private void Register()
    {
        m_testButton = m_root.Q<Button>("test");
        m_addButton = m_root.Q<Button>("add");

        m_testButton.clicked += () => {
            OnTestClicked();
        };

        m_addButton.clicked += () => {
            OnAddClicked();
        };
    }

    private void OnTestClicked()
    {   
        // if(Game.Instance.currentState == InGameStateType.BoardBattleState){
            // m_testButton.text = "Test Signal";
            // GameManager.Instance.ChangeToBoardWaitingState();
        // }else{
            m_testButton.text = "Stop Signal";
            GameManager.Instance.ChangeToBoardBattleState();
        // }
    }

    private void OnAddClicked()
    {
        // if(is AddState){
            // m_testButton.text = "Add Slot";
            // GameManager.Instance.ChangeToAddSlotState();
        // }else{
            m_testButton.text = "Exit Slot";
            GameManager.Instance.ChangeToBoardWaitingState();
        // }
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
