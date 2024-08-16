using UnityEngine;

using Utils;
using Utils.Common;
using InGame.BattleFields.Bullets;

namespace InGame.Views
{
    public class BulletView : MonoBehaviour, IDamager
    {
        private Bullet m_bullet;
        private CountdownTimer m_timer;

        #region LifeCycle
        public void Init(Bullet bullet)
        {
            m_bullet = bullet;
            SetSprite();
            SetTimer();
        }

        private void SetTimer()
        {
            m_timer = new CountdownTimer(m_bullet.lifetime.value); 
            m_timer.OnTimerComplete.AddListener(m_bullet.Die);
            m_timer.StartTimer();
        }

        private void SetSprite()
        {
            Transform model = transform.Find(Constants.MODEL);
            if(model == null)
            {
                Debug.LogError("No model under this game object!");
                return;
            }
            
            if(!model.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                Debug.LogError("No sprite renderer under model!");
                return;
            }

            spriteRenderer.sprite = m_bullet.sprite;
            model.localScale = new(m_bullet.size.value, m_bullet.size.value, 1);
        }

        private void Update()
        {
            Move();
            m_timer.Update(Time.deltaTime); 
        }

        public void Die()
        {
            m_timer.OnTimerComplete.RemoveListener(m_bullet.Die);
            if(gameObject) Destroy(gameObject);
        }
        #endregion

        #region Action
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