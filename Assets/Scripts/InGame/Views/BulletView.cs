using UnityEngine;

using Utils;
using Utils.Common;
using InGame.BattleFields.Androids;

using DG.Tweening;

namespace InGame.Views
{
    public class BulletView : MonoBehaviour, IDamager
    {
        private Bullet m_bullet;
        private Vector3 m_originPosition;
        private float m_distance;

        #region LifeCycle
        public void Init(Bullet bullet)
        {
            m_bullet = bullet;
            m_originPosition = this.transform.position;
            m_distance = Vector3.Distance(this.transform.position, m_bullet.target);
            SetSprite();
        }

        public void SetSprite()
        {
            Transform model = transform.Find(Constants.MODEL);
            if(model == null)
            {
                Debug.LogError("No model under this game object!");
                return;
            }
            SpriteRenderer spriteRenderer = model.GetComponent<SpriteRenderer>();
            if(spriteRenderer == null)
            {
                Debug.LogError("No sprite renderer under model!");
                return;
            }
            spriteRenderer.sprite = m_bullet.sprite;
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
            if(MoveOutOfRange()) Die();

            float currDistance = Vector3.Distance(this.transform.position, m_bullet.target);
            if(currDistance <= Constants.COLLIDE_OFFSET) return;

            transform.DOMove(m_bullet.target, m_distance / m_bullet.speed.value / Constants.SPEED_MULTIPLIER)
                    .SetEase(Ease.OutQuad);
        }

        private bool MoveOutOfRange()
        {
            return Vector3.Distance(this.transform.position, m_originPosition) >= m_bullet.range.value;
        }
        #endregion
        
        #region Interaction
        public void OnTriggerEnter(Collider other)
        {
            int layer = other.gameObject.layer;
            switch(layer)
            {
                case Constants.ANDROID_LAYER:
                    break;
                case Constants.OBSTACLE_LAYER:
                    Die();
                    break;
                default:
                    IDamageable target = other.gameObject.GetComponent<IDamageable>();
                    if(target != null) DealDamage(target, m_bullet.damage.value);
                    break;
            }
        }

        public void DealDamage(IDamageable target, float dmg)
        {
            m_bullet.DealDamage(target, dmg);
        }
        #endregion
    }
}