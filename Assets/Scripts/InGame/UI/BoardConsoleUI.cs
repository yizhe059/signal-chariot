
using UnityEngine;
using UnityEngine.UIElements;

using InGame.Cores;
using Utils.Common;
using InGame.InGameStates;

namespace InGame.UI
{
    public class BoardConsoleUI : MonoBehaviour, IHidable
    {
        [SerializeField] private UIDocument m_doc;
        private VisualElement m_root;
        private Button m_testButton;
        private Button m_slotButton;
        private Button m_exitButton;
        private Button m_marchButton;
        private Label m_name;
        private Label m_category;
        private Label m_desc;
        private Label m_tag;
        private Label m_function;

        private void Awake()
        {
            m_root = m_doc.rootVisualElement;
            Register();
            InitModuleInfo();
        }

        private void Start()
        {
            new AndroidStatusUI(m_root.Q("status")); // stay in start
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
                m_slotButton.text = "扩容";
                m_testButton.text = "测试";
                GameManager.Instance.ChangeToBoardWaitingState();
            }else{
                m_slotButton.text = "扩容";
                m_testButton.text = "退出测试";
                GameManager.Instance.ChangeToBoardTestState();
            }
        }

        private void OnSlotClicked()
        {
            if(GameManager.Instance.GetCurrentInGameState() == InGameStateType.AddSlotState){
                m_slotButton.text = "扩容";
                m_testButton.text = "测试";
                GameManager.Instance.ChangeToBoardWaitingState();
            }else{
                m_slotButton.text = "退出扩容";
                m_testButton.text = "测试";
                GameManager.Instance.ChangeToAddSlotState();
            }
        }

        private void InitModuleInfo()
        {
            m_name = m_root.Q<Label>("name");
            m_category = m_root.Q<Label>("category");
            m_desc = m_root.Q<Label>("description");
            m_tag = m_root.Q<Label>("tag");
            m_function = m_root.Q<Label>("functionality");
            ClearModuleInfo();
        }

        public void DisplayModuleInfo(string[] content)
        {
            m_name.text = content[0];
            m_category.text = content[1];
            m_desc.text = content[2];
            m_tag.text = content[3];
            m_function.text = content[4];
        }

        public void ClearModuleInfo()
        {
            m_name.text = "";
            m_category.text = "";
            m_desc.text = "";
            m_tag.text = "";
            m_function.text = "";
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
