using UnityEngine;

using Utils;
using Utils.Common;

using InGame.BattleFields.Chariots;
using InGame.BattleFields.Common;

namespace InGame.Views
{
    public class ChariotView : MonoBehaviour, IDamageable
    {
        private Vector3 m_moveDirection = Vector3.zero;
        private Vector3 m_obstacleDirection = Vector3.zero;
        private Chariot m_chariot;
        
        #region Life Cycle
        public void Init(Chariot chariot)
        {
            m_chariot = chariot;
        }

        private void Update()
        {
            MoveChariot();
        }

        public void Die()
        {
            Destroy(gameObject);
        }
        #endregion

        #region Action
        private void MoveChariot()
        {
            SetMoveSpeed();
            if (m_moveDirection == Vector3.zero) return;
            this.transform.Translate(m_moveDirection, Space.World);
        }

        public void SetMoveDirection(Vector2 inputDirection)
        {
            float x = inputDirection.x;
            float y = inputDirection.y;

            m_moveDirection = new Vector3(
                x * Mathf.Sqrt(1 - y * y * 0.5f), 
                y * Mathf.Sqrt(1 - x * x * 0.5f), 
                0
            );
            
            if(m_obstacleDirection != Vector3.zero) m_moveDirection = Vector3.zero;
        }

        private void SetMoveSpeed()
        {
            m_moveDirection = (m_moveDirection + m_obstacleDirection).normalized;
            m_moveDirection *= Time.deltaTime *
                            m_chariot.Get(UnlimitedPropertyType.Speed) *
                            Constants.SPEED_MULTIPLIER;
        }

        #endregion

        #region Interaction
        public void TakeDamage(float dmg)
        {
            m_chariot.TakeDamage(dmg);
        }

        public void OnTriggerEnter(Collider other)
        {
            int layer = other.gameObject.layer;
            switch (layer)
            {
                case Constants.MOD_LAYER:
                    PickUp(other.gameObject);
                    break;
                case Constants.OBSTACLE_LAYER:
                    Block(other.transform);
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

        private void PickUp(GameObject item)
        {   
            IPickable target = item.GetComponent<IPickable>();
            target?.PickUp();
        }

        private void Block(Transform obstacleTrans)
        {
            m_obstacleDirection = (this.transform.position - obstacleTrans.position).normalized;
            m_obstacleDirection.z = 0;
        }
        #endregion

        public Vector2 GetPosition()
        {
            return new Vector2(transform.position.x, transform.position.y);
        }
    }
}