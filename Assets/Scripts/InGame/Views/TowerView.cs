using UnityEngine;

using InGame.BattleFields.Chariots;

namespace InGame.Views
{
    public class TowerView : MonoBehaviour
    {
        private Tower m_tower;

        #region Life Cycle
        public void Init(Tower tower)
        {
            // TODO: set sprite
            m_tower = tower;
        }

        private void Update()
        {
            Rotate();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
        #endregion

        #region Action

        private void Rotate()
        {

        }

        #endregion
    }
}