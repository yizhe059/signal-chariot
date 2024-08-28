using System.Collections.Generic;

using UnityEngine;

using SetUps;
using InGame.BattleFields.Androids;
using InGame.Boards.Modules.ModuleBuffs;

namespace InGame.BattleFields.Bullets
{
    public enum BulletType
    {

    }

    public class BulletManager
    {
        private List<List<Bullet>> m_bullets;
        private List<Vector3> m_targets;
        private Dictionary<BulletType, BulletSetUp> m_bulletSetUpLib;

        public BulletManager()
        {
            m_bullets = new();
            m_targets = new();
        }

        public BulletSetUp GenerateBulletSetUp(BulletSetUp bulletSetUp, WeaponBuff buff)
        {
            BulletSetUp resultSetUp = new(bulletSetUp);
            resultSetUp = AddNumericalBuffs(bulletSetUp, buff);
            resultSetUp = AddEffectiveBuffs(bulletSetUp, buff);
            return resultSetUp;
        }

        private BulletSetUp AddNumericalBuffs(BulletSetUp bulletSetUp, WeaponBuff buff)
        {
            // life time
            bulletSetUp.lifeTime *= 1 + (float)buff.lifeTimePercentageBuff/100f;
            bulletSetUp.lifeTime = Mathf.Max(0.001f, bulletSetUp.lifeTime);

            // speed
            bulletSetUp.speed *= 1 + (float)buff.speedPercentageBuff/100f;
            bulletSetUp.speed = Mathf.Max(0.001f, bulletSetUp.speed);

            // damage
            // bulletSetUp.damage *= m_damageMultiplier.value;
            bulletSetUp.damage *= 1 + (float)buff.damagePercentageBuff/100f;
            bulletSetUp.damage += buff.flatDamageBuff;
            bulletSetUp.damage = Mathf.Max(0.001f, bulletSetUp.damage);

            // size
            bulletSetUp.size  *= 1 + (float)buff.bulletSizePercentageBuff/100f;
            bulletSetUp.size = Mathf.Max(0.001f, bulletSetUp.size);
            return bulletSetUp;
        }

        private BulletSetUp AddEffectiveBuffs(BulletSetUp bulletSetUp, WeaponBuff buff)
        {            
            if(buff.bouncingBuff > 0)
            {

            }
            if(buff.penetrationBuff > 0)
            {

            }
            if(buff.splittingBuff > 0)
            {
                
            }
            return bulletSetUp;
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