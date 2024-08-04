using UnityEngine;
using UnityEngine.UIElements;

using InGame.Cores;
using InGame.InGameStates;
using Utils.Common;
using Utils;

namespace InGame.UI
{
    public class BattleResultUI : MonoSingleton<BattleResultUI>, IHidable
    {
        [SerializeField] private UIDocument m_doc;

        private VisualElement m_waveWin;
        private VisualElement m_battleWin;
        private VisualElement m_fail;

        private void Awake()
        {
            m_waveWin = Resources.Load<VisualTreeAsset>(Constants.UI_WAVE_WIN_PATH).Instantiate();
            m_battleWin = Resources.Load<VisualTreeAsset>(Constants.UI_BATTLE_WIN_PATH).Instantiate();
            m_fail = Resources.Load<VisualTreeAsset>(Constants.UI_FAIL_PATH).Instantiate();

            Register();
        }

        private void Register()
        {
            Button waveWinContinue = m_waveWin.Q<Button>("continue");
            Button battleWinContinue = m_battleWin.Q<Button>("continue");
            Button failContinue = m_fail.Q<Button>("continue");

            waveWinContinue.clicked += () => 
            {
                GameManager.Instance.ChangeToBoardWaitingState(); // TODO: Go to next wave
            };

            battleWinContinue.clicked += () => 
            {
                GameManager.Instance.ChangeToBoardWaitingState(); // TODO: Go to next level
            };

            failContinue.clicked += () => 
            {
                GameManager.Instance.Restart(); 
            };
        }

        public void Hide()
        {
            m_doc.rootVisualElement.style.display = DisplayStyle.None;
        }

        public void Show()
        {
            m_doc.rootVisualElement.style.display = DisplayStyle.Flex;
        }

        public void Switch(BattleResultType type)
        {
            VisualElement root = m_doc.rootVisualElement;
            root.Clear();

            switch (type)
            {
                case BattleResultType.WaveWin:
                    root.Add(m_waveWin);
                    break;
                case BattleResultType.BattleWin:
                    root.Add(m_battleWin);
                    break;
                case BattleResultType.GameWin:
                    break;
                case BattleResultType.Fail:
                    root.Add(m_fail);
                    break;
            }
        }
    }
}