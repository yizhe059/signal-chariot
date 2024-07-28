using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

using InGame;
using Utils;
using Utils.Common;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private UIDocument m_doc;
        private VisualElement m_root;
        private Button m_startButton;
        private Button m_testButton;
        private Button m_quitButton;

        private void Awake()
        {
            m_root = m_doc.rootVisualElement;
            Register();
        }

        private void Register()
        {
            m_startButton = m_root.Q<Button>("start");
            m_testButton = m_root.Q<Button>("test");
            m_quitButton = m_root.Q<Button>("quit");

            m_startButton.clicked += () => {
                OnStartPressed();
            };

            m_testButton.clicked += () => {
                OnTestPressed();
            };

            m_quitButton.clicked += () => {
                OnQuitPressed();
            };
        }

        private void OnStartPressed()
        {
            Game.Instance.nextState = new WorldState();
        }

        private void OnTestPressed()
        {
            SceneManager.LoadScene(Constants.TEST);
        }

        private void OnQuitPressed()
        {
            Application.Quit();
        }
    }
}