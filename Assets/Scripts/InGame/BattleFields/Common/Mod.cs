using UnityEngine;

using Utils;
using InGame.Views;
using InGame.Cores;

namespace InGame.BattleFields.Common
{
    public class Mod
    {
        private int m_count;
        private Vector3 m_position;
        private ModView m_view;

        public Mod(int count, Vector2 position)
        {
            m_count = count;
            m_position = position;
            CreateView();
        }

        private void CreateView()
        {
            GameObject chariotPref = Resources.Load<GameObject>(Constants.GO_MOD_PATH);
            GameObject chariotGO = GameObject.Instantiate(chariotPref);
            chariotGO.transform.position = new(m_position.x, m_position.y, Constants.MOD_DEPTH);
            m_view = chariotGO.GetComponent<ModView>();
            m_view.Init(this);
        }

        private void Die()
        {
            m_view.Die();
        }

        public void Consume()
        {
            GameManager.Instance.GetChariot().Increase(UnlimitedPropertyType.Mod, m_count);
            this.Die();
        }
    }
}
