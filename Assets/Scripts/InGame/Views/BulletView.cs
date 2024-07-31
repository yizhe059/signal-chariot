using UnityEngine;

using Utils;
using Utils.Common;
using InGame.BattleFields.Chariots;

using DG.Tweening;

namespace InGame.Views
{
    public class BulletView : MonoBehaviour, IDamager
    {
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
            Destroy(gameObject);
        }
        #endregion

        #region Action
        private void Move()
        {
            float distance = Vector3.Distance(this.transform.position, m_bullet.target);
            if(distance <= Constants.COLLIDE_OFFSET) return;

            transform.DOMove(m_bullet.target, distance / m_bullet.speed.value)
                    .SetEase(Ease.OutCubic);
        }
        #endregion
        
        #region Interaction
        public void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.CompareTag(Constants.CHARIOT_TAG)) return;
            IDamageable target = other.gameObject.GetComponent<IDamageable>();
            if(target != null) DealDamage(target, m_bullet.damage.value);
        }

        public void DealDamage(IDamageable target, float dmg)
        {
            target.TakeDamage(dmg);
            Die();
        }
        #endregion
    }
}