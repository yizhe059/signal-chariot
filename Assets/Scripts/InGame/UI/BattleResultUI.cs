using UnityEngine;
using UnityEngine.UIElements;

using Utils.Common;

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
            m_waveWin = Resources.Load<VisualTreeAsset>("UI/Battle/WaveWin");
            m_battleWin = Resources.Load<VisualTreeAsset>("UI/Battle/BattleWin");
            m_fail = Resources.Load<VisualTreeAsset>("UI/Battle/BattleFail");
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