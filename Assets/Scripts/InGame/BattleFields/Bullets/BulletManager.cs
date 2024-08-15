using System.Collections.Generic;

using UnityEngine;

using SetUps;

namespace InGame.BattleFields.Bullets
{
    public class BulletManager
    {
        private List<Bullet> m_bullets;

        public BulletManager()
        {
            m_bullets = new();
        }

        public Bullet AddBullet(BulletSetUp setup, float dmgMultiplier)
        {
            Bullet bullet = new(setup, dmgMultiplier);
            m_bullets.Add(bullet);
            return bullet;
        }

        public void RemoveBullet(Bullet bullet)
        {
            m_bullets.Remove(bullet);
            bullet.Die();
        }

        public void ClearBullet()
        {
            foreach(Bullet bullet in m_bullets)
            {
                bullet.Die();
            }
            m_bullets.Clear();
        }
    }
}