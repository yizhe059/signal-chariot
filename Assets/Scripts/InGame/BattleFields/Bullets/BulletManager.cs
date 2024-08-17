using System.Collections.Generic;

using UnityEngine;

using SetUps;
using InGame.BattleFields.Androids;

namespace InGame.BattleFields.Bullets
{
    public class BulletManager
    {
        private List<List<Bullet>> m_bullets;
        private List<Vector3> m_targets;

        public BulletManager()
        {
            m_bullets = new();
            m_targets = new();
        }

        public List<Bullet> AddBulletBatch(float batchSize, BulletSetUp setup, Tower tower)
        {
            List<Bullet> batch = new();
            m_bullets.Add(batch);
            m_targets.Add(Vector3.zero);

            int batchIdx = m_bullets.Count-1;

            for(int i = 0; i < batchSize; i++)
            {
                Bullet bullet = new(setup, tower, new int[2]{batchIdx, i});
                batch.Add(bullet);
            }
            
            return batch;
        }

        public void SetBatchInfo(Vector3 target, int batchIdx)
        {
            m_targets[batchIdx] = target;
        }

        public Vector3 GetBatchInfo(int batchIdx)
        {
            return m_targets[batchIdx];
        }

        public int GetBatchSize(int batchIdx)
        {
            return m_bullets[batchIdx].Count;
        }

        public void RemoveBullet(Bullet bullet)
        {
            // m_bullets.Remove(bullet);
            // bullet.Die();
        }

        public void ClearBullet()
        {
            foreach(List<Bullet> batch in m_bullets)
            {
                foreach(Bullet bullet in batch)
                {
                    bullet.Die();
                }
            }
            m_bullets.Clear();
        }
    }
}