using UnityEngine;

using Utils;
using Utils.Common;
using InGame.Cores;
using InGame.BattleFields.Enemies;
using InGame.BattleFields.Chariots;
using InGame.BattleFields.Common;

using DG.Tweening;

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

        public void Die()
        {
            Destroy(gameObject);
        }
        #endregion

        #region Action

        private void Move()
        {
            SetTarget();
            float distance = Vector3.Distance(this.transform.position, m_target);
            if(distance <= Constants.COLLIDE_OFFSET) return;
            // transform.DOMove(m_target, distance / m_enemy.Get(UnlimitedPropertyType.Speed))
                    // .SetEase(Ease.InOutQuad);
            Vector3 direction = (m_target - this.transform.position) 
                                * Time.deltaTime * m_enemy.Get(UnlimitedPropertyType.Speed);
            this.transform.Translate(direction, Space.World);
        }

        private void SetTarget()
        {
            Chariot chariot = GameManager.Instance.GetChariot();
            if(chariot == null) return;
            m_target = chariot.chariotView.transform.position;
            m_target.z = Constants.ENEMY_DEPTH;
        }

        #endregion
        
        #region Interaction
        public void OnTriggerEnter(Collider other)
        {
            IDamageable target = other.gameObject.GetComponent<IDamageable>();
            if(target != null) DealDamage(target, m_enemy.Get(UnlimitedPropertyType.Damage));
        }

        public void TakeDamage(float dmg)
        {
            m_enemy.TakeDamage(dmg);
        }

        public void DealDamage(IDamageable target, float dmg)
        {
            target.TakeDamage(dmg);
        }

        #endregion

        public void SetPosition(Vector2 pos)
        {
            Vector3 worldPos = pos;
            worldPos.z = Constants.ENEMY_DEPTH;
            transform.position = worldPos;
        }
    }
}