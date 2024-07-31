using UnityEngine;
using UnityEngine.UIElements;

using InGame.InGameStates;
using Utils.Common;
using Utils;

namespace InGame.UI
{
    public class BattleResultUI : MonoSingleton<BattleResultUI>, IHidable
    {
        [SerializeField] private UIDocument m_doc;

        private VisualTreeAsset m_waveWin;
        private VisualTreeAsset m_battleWin;
        private VisualTreeAsset m_fail;

        private void Awake()
        {
            m_waveWin = Resources.Load<VisualTreeAsset>(Constants.UI_WAVE_WIN_PATH);
            m_battleWin = Resources.Load<VisualTreeAsset>(Constants.UI_BATTLE_WIN_PATH);
            m_fail = Resources.Load<VisualTreeAsset>(Constants.UI_FAIL_PATH);
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
                    root.Add(m_waveWin.Instantiate());
                    break;
                case BattleResultType.BattleWin:
                    root.Add(m_battleWin.Instantiate());
                    break;
                case BattleResultType.GameWin:
                    break;
                case BattleResultType.Fail:
                    root.Add(m_fail.Instantiate());
                    break;
            }
        }
    }
}