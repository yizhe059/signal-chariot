using UnityEngine;

using Utils;
using Utils.Common;

using InGame.BattleFields.Androids;
using InGame.BattleFields.Common;

namespace InGame.Views
{
    public class AndroidView : MonoBehaviour, IDamageable
    {
        private Vector3 m_moveDirection = Vector3.zero;
        private Vector3 m_obstacleDirection = Vector3.zero;
        private Android m_android;
        private float m_length = .5f;
        
        #region Life Cycle
        public void Init(Android android)
        {
            m_android = android;
        }

        private void Update()
        {
            GenerateRay();
            Move();
        }

        public void Die()
        {
            Destroy(gameObject);
        }
        #endregion

        #region Action
        private void GenerateRay()
        {
            RaycastHit[] hits = new RaycastHit[4];
            Physics.Raycast(transform.position, Vector2.up, out hits[0], m_length);
            Physics.Raycast(transform.position, Vector2.down, out hits[1], m_length);
            Physics.Raycast(transform.position, Vector2.left, out hits[2], m_length);
            Physics.Raycast(transform.position, Vector2.right, out hits[3], m_length);
            if ((hits[0].collider != null && m_moveDirection.y > 0) ||
                (hits[1].collider != null && m_moveDirection.y < 0))
                m_moveDirection.y = 0;  
            if ((hits[2].collider != null && m_moveDirection.x < 0) ||
                (hits[3].collider != null && m_moveDirection.x > 0))
                m_moveDirection.x = 0;  
        }

        public void SetMoveDirection(Vector2 inputDirection)
        {
            float x = inputDirection.x;
            float y = inputDirection.y;
            m_moveDirection = new Vector3(x * Mathf.Sqrt(1 - y * y * 0.5f), 
                                        y * Mathf.Sqrt(1 - x * x * 0.5f), 0);
        }

        private void Move()
        {
            m_moveDirection = (m_moveDirection + m_obstacleDirection).normalized;
            Vector3 velocity = Constants.SPEED_MULTIPLIER * 
                            m_android.Get(UnlimitedPropertyType.Speed) * 
                            Time.deltaTime * m_moveDirection;

            if (velocity == Vector3.zero) return;
            this.transform.Translate(velocity, Space.World);
        }                                                                     
        #endregion

        #region Interaction
        public void TakeDamage(float dmg)
        {
            m_android.TakeDamage(dmg);
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
                    break;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            int layer = other.gameObject.layer;
            switch(layer)
            {
                case Constants.OBSTACLE_LAYER:
                    break;
            }
        }

        private void PickUp(GameObject item)
        {   
            IPickable target = item.GetComponent<IPickable>();
            target?.PickUp();
        }
        #endregion

        public Vector2 GetPosition()
        {
            return new Vector2(transform.position.x, transform.position.y);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * m_length);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * m_length);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.left * m_length);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * m_length);
        }
    }
}