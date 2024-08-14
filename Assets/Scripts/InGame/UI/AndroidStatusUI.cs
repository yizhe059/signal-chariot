using InGame.BattleFields.Androids;
using InGame.BattleFields.Common;
using InGame.Cores;
using UnityEngine;
using UnityEngine.UIElements;

using Utils.Common;

namespace InGame.UI
{
    public class AndroidStatusUI : MonoSingleton<AndroidStatusUI>, IHidable
    {
        [SerializeField] private UIDocument m_doc;
        private VisualElement m_root;
        private VisualElement m_health;
        private VisualElement m_defence;
        private VisualElement m_mod;
        
        private void Awake()
        {
            m_root = m_doc.rootVisualElement;
            m_health = m_root.Q("health");
            m_defence = m_root.Q("defence");
            m_mod = m_root.Q("mod");
        }

        private void Start()
        {
            Android android = GameManager.Instance.GetAndroid();
            android.RegisterPropertyEvent(LimitedPropertyType.Health, SetHealthUI);
            android.RegisterPropertyEvent(UnlimitedPropertyType.Defence, SetDefenceUI);
            android.RegisterPropertyEvent(UnlimitedPropertyType.Mod, SetModUI);
        }

        private void SetHealthUI(float current, float max)
        {
            ProgressBar bar = m_health.Q<ProgressBar>("bar");
            bar.highValue = max;
            bar.value = current;
            bar.title = $"{current}/{max}";
        }

        private void SetDefenceUI(float current)
        {
            Label content = m_defence.Q<Label>("content");
            content.text = $"DEFENCE: {current}";
        }

        private void SetModUI(float current)
        {
            Label content = m_mod.Q<Label>("content");
            content.text = $"MOD: {current}";
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