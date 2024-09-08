using System;
using UnityEngine;

namespace InGame.Views
{
    public class FloatingText: MonoBehaviour
    {
        private float m_fadingDuration;
        private float m_fadingTimer = 0;
        private bool m_isDisplaying = false;

        private TextMesh m_textMesh;
        private TextMesh textMesh
        {
            get
            {
                if (m_textMesh == null) m_textMesh = GetComponent<TextMesh>();
                return m_textMesh;
            }
            
        }

        public void Init(float fadingDuration)
        {
            m_fadingDuration = fadingDuration;
        }

        public void ClearText()
        {
            m_isDisplaying = false;
            textMesh.text = "";
            m_fadingTimer = 0f;
        }

        public void DisplayText(int number)
        {
            m_isDisplaying = true;
            textMesh.text = $"{number}";
            m_fadingTimer = 0f;
            
        }

        public void Update()
        {
            if (!m_isDisplaying) return;

            m_fadingTimer += Time.deltaTime;

            Color originalColor = textMesh.color;
            originalColor.a = 1f;
            
            Color fadedColor = originalColor;
            fadedColor.a = 0;

            var displayColor = Color.Lerp(originalColor, fadedColor, m_fadingTimer / m_fadingDuration);

            textMesh.color = displayColor;

            if (m_fadingTimer >= m_fadingDuration)
            {
                ClearText();
            }
        }
    }
}