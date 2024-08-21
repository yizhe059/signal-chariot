using InGame.BattleFields.Androids;
using InGame.BattleFields.Common;
using InGame.Cores;
using UnityEngine;
using UnityEngine.UIElements;

using Utils.Common;

namespace InGame.UI
{
    public class AndroidStatusUI : IHidable
    {
        private VisualElement m_root;
        private VisualElement m_health;
        private VisualElement m_defence;
        private VisualElement m_mod;
        private VisualElement m_crystal;
        
        public AndroidStatusUI(VisualElement root)
        {
            m_root = root;
            m_health = m_root.Q("health");
            m_defence = m_root.Q("defence");
            m_mod = m_root.Q("mod");
            m_crystal = m_root.Q("crystal");
            Register();
        }

        private void Register()
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

        private void SetCrystalUI(float current)
        {
            Label content = m_crystal.Q<Label>("content");
            content.text = $"CRYSTAL: {current}";
        }

        public void Hide()
        {
            m_root.style.display = DisplayStyle.None;
        }

        public void Show()
        {
            m_root.style.display = DisplayStyle.Flex;
        }
    }
}