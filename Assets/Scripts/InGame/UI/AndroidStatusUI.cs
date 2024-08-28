using System;
using InGame.BattleFields.Androids;
using InGame.BattleFields.Common;
using InGame.Cores;

using UnityEngine.UIElements;

using Utils.Common;

namespace InGame.UI
{
    public class AndroidStatusUI : IHidable
    {
        private VisualElement m_root;
        private VisualElement m_health;
        private VisualElement m_defense;
        private VisualElement m_armor;
        private VisualElement m_mod;
        private VisualElement m_crystal;
        
        public AndroidStatusUI(VisualElement root)
        {
            m_root = root;
            m_health = m_root.Q("health");
            m_defense = m_root.Q("defense");
            m_armor = m_root.Q("armor");
            m_mod = m_root.Q("mod");
            m_crystal = m_root.Q("crystal");
            Register();
        }

        private void Register()
        {
            Android android = GameManager.Instance.GetAndroid();
            android.RegisterPropertyEvent(LimitedPropertyType.Health, SetHealthUI);
            android.RegisterPropertyEvent(UnlimitedPropertyType.Defense, SetDefenseUI);
            android.RegisterPropertyEvent(UnlimitedPropertyType.Armor, SetArmorUI);
            android.RegisterPropertyEvent(UnlimitedPropertyType.Mod, SetModUI);
            android.RegisterPropertyEvent(UnlimitedPropertyType.Crystal, SetCrystalUI);
        }

        ~AndroidStatusUI()
        {
            Android android = GameManager.Instance.GetAndroid();
            android.UnregisterPropertyEvent(LimitedPropertyType.Health, SetHealthUI);
            android.UnregisterPropertyEvent(UnlimitedPropertyType.Defense, SetDefenseUI);
            android.UnregisterPropertyEvent(UnlimitedPropertyType.Armor, SetArmorUI);
            android.UnregisterPropertyEvent(UnlimitedPropertyType.Mod, SetModUI);
            android.UnregisterPropertyEvent(UnlimitedPropertyType.Crystal, SetCrystalUI);
        }        

        private void SetHealthUI(float current, float max)
        {
            ProgressBar bar = m_health.Q<ProgressBar>("bar");
            bar.highValue = max;
            bar.value = current;
            bar.title = $"{current}/{max}";
        }

        private void SetDefenseUI(float current)
        {
            Label content = m_defense.Q<Label>("content");
            content.text = $"DEFENSE: {current}";
        }

        private void SetArmorUI(float current)
        {
            Label content = m_armor.Q<Label>("content");
            content.text = $"ARMOR: {current}";
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