using UnityEngine;

using InGame.BattleFields.Chariots;

using DG.Tweening;

namespace InGame.Views
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField] private float COLLIDE_OFFSET = 0.1f;
        private Bullet m_bullet;

        public void Init(Bullet bullet)
        {
            m_bullet = bullet;
        }

        private void Update()
        {
            MoveBullet();
        }

        private void MoveBullet()
        {
            float distance = Vector3.Distance(this.transform.position, m_bullet.target);
            if(distance <= COLLIDE_OFFSET) return;

            transform.DOMove(m_bullet.target, distance / m_bullet.speed.value)
                    .SetEase(Ease.OutCubic);
        }
    }
}