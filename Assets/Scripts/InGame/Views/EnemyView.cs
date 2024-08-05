using UnityEngine;

using Utils;
using Utils.Common;
using InGame.UI;
using InGame.Cores;
using InGame.BattleFields.Enemies;
using InGame.BattleFields.Chariots;
using InGame.BattleFields.Common;

namespace InGame.Views
{
    public class EnemyView : MonoBehaviour, IDamager, IDamageable
    {
        private Enemy m_enemy;
        private Vector3 m_target = Vector3.zero;
        private Vector3 m_direction = Vector3.zero;
        private Vector3 m_obstacleDirection = Vector3.zero;
        private bool m_isOn = false;
        private SlideBarUI m_healthBar;

        #region Life Cycle
        public void Init(Enemy enemy)
        {
            m_enemy = enemy;
            m_healthBar = new(transform.Find("Canvas").gameObject);
        }

        private void Update()
        {
            if (!m_isOn) return;
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
            if(chariot == null || chariot.chariotView == null) return;
            
            m_target = chariot.chariotView.transform.position;
            m_target.z = Constants.ENEMY_DEPTH;

            float distance = Vector3.Distance(this.transform.position, m_target);
            if(distance <= Constants.COLLIDE_OFFSET) return;

            m_direction = (m_target - this.transform.position).normalized;
            
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

            if(m_obstacleDirection != Vector3.zero) m_direction = Vector3.zero;

            m_direction = (m_direction + seperation + m_obstacleDirection).normalized;
            m_direction *= Time.deltaTime * 
                        m_enemy.Get(UnlimitedPropertyType.Speed) * 
                        Constants.SPEED_MULTIPLIER;
            
            this.transform.Translate(m_direction, Space.World);
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
                    // TODO: 
                    // 1. enlarge collider size based on attack range
                    // 2. attack multiple times
                    IDamageable target = other.gameObject.GetComponent<IDamageable>();
                    if(target != null) DealDamage(target, m_enemy.Get(UnlimitedPropertyType.Damage));
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