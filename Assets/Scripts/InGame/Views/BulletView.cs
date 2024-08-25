using UnityEngine;

using Utils;
using Utils.Common;
using InGame.BattleFields.Bullets;

namespace InGame.Views
{
    public class BulletView : MonoBehaviour, IDieable, IDamager
    {
        private Bullet m_bullet;
        private CountdownTimer m_timer;
        private Transform m_model;
        private SpriteRenderer m_spriteRenderer;
        private Collider m_collider;

        #region LifeCycle
        public void Init(Bullet bullet)
        {
            m_bullet = bullet;
            SetSprite();
            SetCollider();
            SetTimer();
        }

        private void Update()
        {
            Move();
            m_timer.Update(Time.deltaTime); 
        }

        public void Die()
        {
            m_timer.OnTimerComplete.RemoveListener(m_bullet.Die);
            Destroy(gameObject);
        }
        #endregion

        #region Action
        private void SetTimer()
        {
            m_timer = new CountdownTimer(m_bullet.lifetime.value); 
            m_timer.OnTimerComplete.AddListener(m_bullet.Die);
            m_timer.StartTimer();
        }

        private void SetSprite()
        {
            m_model = transform.Find(Constants.MODEL);
            if(m_model == null)
            {
                Debug.LogError("No model under this game object!");
                return;
            }
            m_model.localScale = new(m_bullet.size.value, m_bullet.size.value, 1);

            if(!m_model.TryGetComponent<SpriteRenderer>(out m_spriteRenderer))
            {
                Debug.LogError("No sprite renderer under model!");
                return;
            }
            m_spriteRenderer.sprite = m_bullet.sprite;
        }

        private void SetCollider()
        {
            if(!transform.TryGetComponent<Collider>(out m_collider))
                Debug.LogError("No collider for bullet!");
        }

        public void Disable()
        {
            m_spriteRenderer.enabled = false;
            m_collider.enabled = false;
        }

        public void Enable()
        {
            m_spriteRenderer.enabled = true;
            m_collider.enabled = true;
        }

        private void Move()
        {
            m_bullet.moveStrategy.Move();
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
                    Die(); // TODO effect
                    break;
                default:
                    // TODO effect
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