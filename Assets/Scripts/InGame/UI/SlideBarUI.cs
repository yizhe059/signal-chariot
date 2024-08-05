using System.Collections;
using System.Collections.Generic;
using InGame.BattleFields.Common;
using UnityEngine;
using UnityEngine.UI;

using Utils.Common;

namespace InGame.UI
{
    public class SlideBarUI : IHidable
    {
        private GameObject m_slideBar;
        private Image m_max;
        private Image m_value;

        public SlideBarUI(GameObject obj, IPropertyRegisterable register,           
                        LimitedPropertyType type)
        {
            GameObject canvas = obj.transform.Find("Canvas").gameObject;
            m_slideBar = canvas.transform.Find("SlideBar").gameObject;

            m_max = m_slideBar.transform.Find("max").GetComponent<Image>();
            m_value = m_slideBar.transform.Find("value").GetComponent<Image>();

            register.RegisterPropertyEvent(type, SetBarUI);
        }

        private void SetBarUI(float value, float max)
        {
            m_value.fillAmount = value / max;
        }

        public void Hide()
        {
            throw new System.NotImplementedException();
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }
    }
}
