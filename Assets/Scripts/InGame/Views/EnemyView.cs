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
            Chariot chariot = GameManager.Instance.GetChariot();
            if(chariot == null) return;
            
            m_target = chariot.chariotView.transform.position;
            m_target.z = Constants.ENEMY_DEPTH;

            float distance = Vector3.Distance(this.transform.position, m_target);
            if(distance <= Constants.COLLIDE_OFFSET) return;

            Vector3 direction = (m_target - this.transform.position).normalized;
            
            Vector3 seperation = Vector3.zero;
            foreach(Enemy otherEnemy in GameManager.Instance.GetEnemySpawnController().GetAllEnemies())
            {
                if(otherEnemy == null) continue;
                GameObject otherEnemyGO = otherEnemy.GetView().gameObject;
                if(otherEnemyGO == this.gameObject) continue;
                
                Vector3 directionToOther = otherEnemyGO.transform.position - transform.position;
                float distanceToOther = directionToOther.magnitude;
                if(distanceToOther < Constants.SEPERATION_DISTANCE)
                    seperation -= (directionToOther / distanceToOther) * 
                                (Constants.SEPERATION_DISTANCE - distanceToOther) * 
                                Constants.SEPERATION_FORCE;
                
            }

            direction = (direction + seperation).normalized;
            direction *= Time.deltaTime * 
                        m_enemy.Get(UnlimitedPropertyType.Speed) * 
                        Constants.SPEED_MULTIPLIER;
            this.transform.Translate(direction, Space.World);
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