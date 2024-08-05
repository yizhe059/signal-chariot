using System.Collections;
using System.Collections.Generic;

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

        public SlideBarUI(GameObject canvas)
        {
            m_slideBar = canvas.transform.Find("SlideBar").gameObject;

            m_max = m_slideBar.transform.Find("max").GetComponent<Image>();
            m_value = m_slideBar.transform.Find("value").GetComponent<Image>();
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
