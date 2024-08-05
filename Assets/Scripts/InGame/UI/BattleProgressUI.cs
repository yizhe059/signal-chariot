using UnityEngine;
using UnityEngine.UIElements;

using Utils.Common;

namespace InGame.UI
{
    public class BattleProgressUI : MonoSingleton<BattleProgressUI>, IHidable
    {
        [SerializeField] private UIDocument m_doc;
        private VisualElement m_root;
        private ProgressBar m_time;
        private ProgressBar m_enemy;

        private void Awake()
        {
            m_root = m_doc.rootVisualElement;
            m_time = m_root.Q<ProgressBar>("timeBar");
            m_enemy = m_root.Q<ProgressBar>("enemyBar");
        }

        private void Start()
        {
            
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