using UnityEngine;
using UnityEngine.UIElements;

using InGame.Cores;
using Utils.Common;
using InGame.BattleFields.Enemies;

namespace InGame.UI
{
    public class BattleProgressUI : MonoSingleton<BattleProgressUI>, IHidable
    {
        [SerializeField] private UIDocument m_doc;
        private VisualElement m_root;
        private ProgressBar m_time;
        private ProgressBar m_enemy;
        private EnemySpawnController m_enemyController;

        private void Awake()
        {
            m_root = m_doc.rootVisualElement;
            m_time = m_root.Q<ProgressBar>("timeBar");
            m_enemy = m_root.Q<ProgressBar>("enemyBar");
        }

        private void Start()
        {
            m_enemyController = GameManager.Instance.GetEnemySpawnController();
        }

        private void Update()
        {
            SetTimeUI();
        }

        private void SetTimeUI()
        {
            m_time.highValue = m_enemyController.GetCurrentWaveTotalDuration();
            m_time.value = m_enemyController.GetCurrentWaveTimer();
            m_time.title = $"{m_time.value}/{m_time.highValue}";
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