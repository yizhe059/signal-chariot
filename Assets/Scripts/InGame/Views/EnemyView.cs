using UnityEngine;

using Utils;
using Utils.Common;
using InGame.UI;
using InGame.Cores;
using InGame.BattleFields.Enemies;
using InGame.BattleFields.Androids;
using InGame.BattleFields.Common;
using System.Collections;

namespace InGame.Views
{
    public class EnemyView : MonoBehaviour, IDamager, IDamageable
    {
        private Enemy m_enemy;
        private Vector3 m_target = Vector3.zero;
        private Vector3 m_direction = Vector3.zero;
        private Vector3 m_obstacleDirection = Vector3.zero;
        private bool m_isOn = false;
        private IDamageable m_dmgTarget = null;

        #region Life Cycle
        public void Init(Enemy enemy)
        {
            m_enemy = enemy;
            new SlideBarUI(gameObject, m_enemy, LimitedPropertyType.Health);
        }

        private void Update()
        {
            if(!m_isOn) return;
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
            Android android = GameManager.Instance.GetAndroid();
            if(android == null || android.androidView == null) return;
            
            m_target = android.androidView.transform.position;
            m_target.z = Constants.ENEMY_DEPTH;

            float distance = Vector3.Distance(this.transform.position, m_target);
            if(distance <= Constants.COLLIDE_OFFSET) return;

            m_direction = (m_obstacleDirection != Vector3.zero) ? 
                        Vector3.zero : (m_target - this.transform.position).normalized;

            m_direction = (m_direction + Seperation() + m_obstacleDirection).normalized;
            m_direction *= Time.deltaTime * 
                        m_enemy.Get(UnlimitedPropertyType.Speed) * 
                        Constants.SPEED_MULTIPLIER;
            
            this.transform.Translate(m_direction, Space.World);
        }

        private Vector3 Seperation()
        {
            Vector3 seperation = Vector3.zero;
            foreach(Enemy otherEnemy in GameManager.Instance.GetEnemySpawnController().GetAllEnemies())
            {
                if(otherEnemy == null) continue;
                GameObject otherEnemyGO = otherEnemy.GetView().gameObject;
                if(otherEnemyGO == this.gameObject) continue;

                Vector3 directionToOther = otherEnemyGO.transform.position - transform.position;
                float distanceToOther = directionToOther.magnitude;
                if(distanceToOther < Constants.SEPERATION_DISTANCE)
                    seperation -= directionToOther / distanceToOther * 
                                (Constants.SEPERATION_DISTANCE - distanceToOther) * 
                                Constants.SEPERATION_FORCE;
            }
            return seperation;
        }

        #endregion
        
        #region Interaction
        public void OnTriggerEnter(Collider other)
        {
            int layer = other.gameObject.layer;
            switch(layer)
            {
                case Constants.ENEMY_LAYER:
                    break;
                case Constants.OBSTACLE_LAYER:
                    Block(other.transform);
                    break;
                default:
                    m_dmgTarget = other.gameObject.GetComponent<IDamageable>();
                    if(m_dmgTarget != null) StartCoroutine(Attack());
                    break;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            int layer = other.gameObject.layer;
            switch(layer)
            {
                case Constants.OBSTACLE_LAYER:
                    m_obstacleDirection = Vector3.zero;
                    break;
                default:
                    m_dmgTarget = null;
                    break;
            }
        }

        private void Block(Transform obstacleTrans)
        {
            Vector3 collisionPoint = gameObject.GetComponent<Collider>().ClosestPoint(obstacleTrans.position);
            m_obstacleDirection = (this.transform.position - collisionPoint).normalized;
            m_obstacleDirection.z = 0;
        }

        public void TakeDamage(float dmg)
        {
            m_enemy.TakeDamage(dmg);
        }

        public IEnumerator Attack()
        {
            // TODO: enlarge collider size based on attack range
            DealDamage(m_dmgTarget, m_enemy.Get(UnlimitedPropertyType.Damage));
            yield return new WaitForSeconds(m_enemy.Get(UnlimitedPropertyType.Interval));
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
        
        public void TurnOn() => m_isOn = true;

        public void TurnOff() => m_isOn = false;
    }
}