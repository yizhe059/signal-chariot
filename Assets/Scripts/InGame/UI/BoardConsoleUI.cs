
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
        private VisualElement m_screen;
        private VisualElement m_androidStatus;
        private Button m_testButton;
        private Button m_slotButton;
        private Button m_exitButton;
        private Button m_marchButton;

        private void Awake()
        {
            m_root = m_doc.rootVisualElement;
            AddAndroidStatus();
            Register();
        }

        private void AddAndroidStatus()
        {
            m_androidStatus = Resources.Load<VisualTreeAsset>(Constants.UI_ANDROID_STATUS_PATH).Instantiate();
            m_screen = m_root.Q("screen");
            m_screen.Add(m_androidStatus);
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
