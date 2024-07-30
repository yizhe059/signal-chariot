using UnityEngine;

using Utils.Common;
using InGame.BattleFields.Chariots;

using DG.Tweening;

namespace InGame.Views
{
    public class BulletView : MonoBehaviour, IDamager
    {
        [SerializeField] private float COLLIDE_OFFSET = 0.1f;
        private Bullet m_bullet;

        #region LifeCycle
        public void Init(Bullet bullet)
        {
            // TODO: set sprite
            m_bullet = bullet;
        }

        private void Update()
        {
            Move();
        }

        private void Die()
        {

        }
        #endregion

        #region Action
        private void Move()
        {
            float distance = Vector3.Distance(this.transform.position, m_bullet.target);
            if(distance <= COLLIDE_OFFSET) return;

            transform.DOMove(m_bullet.target, distance / m_bullet.speed.value)
                    .SetEase(Ease.OutCubic);
        }
        #endregion
        
        #region Interaction
        public void DealDamage(IDamageable target, float dmg)
        {
            target.TakeDamage(dmg);
            Die();
        }

        #endregion
    }
}