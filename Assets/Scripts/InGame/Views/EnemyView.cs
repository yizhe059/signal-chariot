using UnityEngine;

using Utils;
using Utils.Common;
using InGame.BattleFields.Enemies;

using DG.Tweening;
using Unity.VisualScripting;

namespace InGame.Views
{
    public class EnemyView : MonoBehaviour, IDamager, IDamageable
    {
        private Enemy m_enemy;
        private Vector3 m_target = Vector3.zero;

        #region Life Cycle
        public void Init(Enemy enemy)
        {
            m_enemy = enemy;
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
            float distance = Vector3.Distance(this.transform.position, m_target);
            if(distance <= Constants.COLLIDE_OFFSET) return;
            transform.DOMove(m_target, distance / m_enemy.speed.value)
                    .SetEase(Ease.InOutQuad);
        }

        #endregion

        #region Interaction
        public void OnCollisionEnter(Collision other)
        {
            IDamageable target = other.gameObject.GetComponent<IDamageable>();
            if(target != null) DealDamage(target, m_enemy.damage.value);
        }

        public void TakeDamage(float dmg)
        {
            m_enemy.health.DecreaseCurrent(dmg);
            if(m_enemy.health.current <= 0) Die();
        }

        public void DealDamage(IDamageable target, float dmg)
        {
            target.TakeDamage(dmg);
        }

        #endregion
    }
}