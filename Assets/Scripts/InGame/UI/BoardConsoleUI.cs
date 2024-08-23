
using UnityEngine;
using UnityEngine.UIElements;

using InGame.Cores;
using Utils.Common;
using InGame.InGameStates;
using Utils;

namespace InGame.UI
{
    public class BoardConsoleUI : MonoBehaviour, IHidable
    {
        [SerializeField] private UIDocument m_doc;
        private VisualElement m_root;
        private AndroidStatusUI m_status;
        private Button m_testButton;
        private Button m_slotButton;
        private Button m_exitButton;
        private Button m_marchButton;

        private void Awake()
        {
            m_root = m_doc.rootVisualElement;
            Register();
        }

        private void Start()
        {
            m_status = new AndroidStatusUI(m_root.Q("status")); // stay in start
        }

        private void Register()
        {
            m_testButton = m_root.Q<Button>("test");
            m_slotButton = m_root.Q<Button>("add");
            m_exitButton = m_root.Q<Button>("exit");
            m_marchButton = m_root.Q<Button>("march");

            m_testButton.clicked += () => {
                OnSignalClicked();
            };

            m_slotButton.clicked += () => {
                OnSlotClicked();
            };

            m_exitButton.clicked += () => {
                GameManager.Instance.ChangeToInitState();
            };

            m_marchButton.clicked += () => {
                GameManager.Instance.ChangeToBattleState();
            };
        }

        private void OnSignalClicked()
        {   
            if(GameManager.Instance.GetCurrentInGameState() == InGameStateType.BoardTestState){
                m_slotButton.text = "Add Slot";
                m_testButton.text = "Test Signal";
                GameManager.Instance.ChangeToBoardWaitingState();
            }else{
                m_slotButton.text = "Add Slot";
                m_testButton.text = "Stop Signal";
                GameManager.Instance.ChangeToBoardTestState();
            }
        }

        private void OnSlotClicked()
        {
            if(GameManager.Instance.GetCurrentInGameState() == InGameStateType.AddSlotState){
                m_slotButton.text = "Add Slot";
                m_testButton.text = "Test Signal";
                GameManager.Instance.ChangeToBoardWaitingState();
            }else{
                m_slotButton.text = "Exit Slot";
                m_testButton.text = "Test Signal";
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
