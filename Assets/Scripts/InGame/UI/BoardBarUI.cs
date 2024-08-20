
using UnityEngine;
using UnityEngine.UIElements;

using InGame.Cores;
using Utils.Common;
using InGame.InGameStates;

namespace InGame.UI
{
    public class BoardBarUI : MonoBehaviour, IHidable
    {
        [SerializeField] private UIDocument m_doc;
        private VisualElement m_root;
        private Button m_signalButton;
        private Button m_slotButton;

        private void Awake()
        {
            m_root = m_doc.rootVisualElement;
            Register();
        }

        private void Register()
        {
            m_signalButton = m_root.Q<Button>("test");
            m_slotButton = m_root.Q<Button>("add");

            m_signalButton.clicked += () => {
                OnSignalClicked();
            };

            m_slotButton.clicked += () => {
                OnSlotClicked();
            };
        }

        private void OnSignalClicked()
        {   
            if(GameManager.Instance.GetCurrentInGameState() == InGameStateType.BoardTestState){
                m_slotButton.text = "Add Slot";
                m_signalButton.text = "Test Signal";
                GameManager.Instance.ChangeToBoardWaitingState();
            }else{
                m_slotButton.text = "Add Slot";
                m_signalButton.text = "Stop Signal";
                GameManager.Instance.ChangeToBoardTestState();
            }
        }

        private void OnSlotClicked()
        {
            if(GameManager.Instance.GetCurrentInGameState() == InGameStateType.AddSlotState){
                m_slotButton.text = "Add Slot";
                m_signalButton.text = "Test Signal";
                GameManager.Instance.ChangeToBoardWaitingState();
            }else{
                m_slotButton.text = "Exit Slot";
                m_signalButton.text = "Test Signal";
                GameManager.Instance.ChangeToAddSlotState();
            }
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
