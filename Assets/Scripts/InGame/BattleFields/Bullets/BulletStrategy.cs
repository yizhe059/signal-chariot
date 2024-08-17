using UnityEngine;
using DG.Tweening;

using Utils;
using InGame.BattleFields.Enemies;
using InGame.Cores;
using Unity.VisualScripting;

namespace InGame.BattleFields.Bullets
{
    public enum MoveType
    {
        Linear,
        Follow,
        Parabola,
        CircleRound,
        Placement,
        Random,
    }
    public enum DamageType
    {
        Collide,
        Range,
    }
    
    public interface IMovable
    {
        void Move();
    }

    public abstract class MoveStrategy
    {
        protected Bullet m_bullet;
        protected Transform m_bulletTransform;
        protected int m_batchIdx;
        protected int m_bulletIdx;
        protected BulletManager m_bulletManager;

        public MoveStrategy(Bullet bullet)
        {
            this.m_bullet = bullet;
            this.m_bulletTransform = bullet.bulletView.transform;
            this.m_batchIdx = bullet.bulletIdx[0];
            this.m_bulletIdx = bullet.bulletIdx[1];
            this.m_bulletManager = bullet.tower.bulletManager;
            if(m_bulletIdx == 0) this.SetBatchInfo();
        }

        protected virtual void SetBatchInfo(){}
    }

    public class LinearMoveStrategy : MoveStrategy, IMovable
    {
        private Vector3 m_velocity;
    
        public LinearMoveStrategy(Bullet bullet) : base(bullet)
        {
            this.SetDirection();
        }

        protected override void SetBatchInfo()
        {
            Enemy closest = GameManager.Instance.GetEnemySpawnController().
                            GetClosestEnemy(m_bulletTransform.position);

            Vector3 target;
            if(closest != null) target = closest.GetView().transform.position;
            else target = Utilities.RandomPosition();

            m_bulletManager.SetBatchInfo(target, m_batchIdx);
            m_bullet.tower.towerView.SetTarget(target);
        }

        private void SetDirection()
        {
            Vector3 target = m_bulletManager.GetBatchInfo(m_batchIdx);
            Vector3 direction = target - m_bulletTransform.position;
            direction.z = Constants.BULLET_DEPTH;

            m_velocity = Constants.SPEED_MULTIPLIER * m_bullet.speed.value * 
                        Time.deltaTime * direction.normalized;
        }

        public void Move()
        {
            m_bulletTransform.Translate(m_velocity, Space.World);
        }
    }

    public class RandomMoveStrategy : MoveStrategy, IMovable
    {  
        private Vector3 m_velocity;

        public RandomMoveStrategy(Bullet bullet) : base(bullet)
        {
            this.SetDirection();
        }

        protected override void SetBatchInfo()
        {
            Vector3 target = Utilities.RandomPosition();
            m_bulletManager.SetBatchInfo(target, m_batchIdx);
            m_bullet.tower.towerView.SetTarget(target);
        }

        private void SetDirection()
        {
            Vector3 target = m_bulletManager.GetBatchInfo(m_batchIdx);
            Vector3 direction = (target - m_bulletTransform.position).normalized;
            m_velocity = Constants.SPEED_MULTIPLIER * m_bullet.speed.value * 
                        Time.deltaTime * direction;
        }

        public void Move()
        {
            m_bulletTransform.Translate(m_velocity, Space.World);
        }
    }

    public class FollowMoveStrategy : MoveStrategy, IMovable
    {
        private Transform m_target;

        public FollowMoveStrategy(Bullet bullet) : base(bullet)
        {
            this.SetTarget();
        }

        private void SetTarget()
        {
            Enemy closest = GameManager.Instance.GetEnemySpawnController().
                            GetClosestEnemy(m_bulletTransform.position);
            if(closest == null){
                Debug.LogWarning("No Target to Follow!");
                return;
            }

            m_target = closest.GetView().transform;
            m_target.position = new Vector3(
                m_target.position.x,
                m_target.position.y,
                Constants.BULLET_DEPTH
            );
            
            m_bullet.tower.towerView.SetTarget(m_target.position);
        }

        public void Move()
        {
            if(m_target == null) this.SetTarget();
            if(m_target == null) return;
            Vector3 direction = m_target.position - m_bulletTransform.position;
            direction.z = Constants.BULLET_DEPTH;
            Vector3 velocity = Constants.SPEED_MULTIPLIER * m_bullet.speed.value * 
                                Time.deltaTime * direction.normalized;
            m_bulletTransform.Translate(velocity, Space.World);
        }
    }

    public class ParabolaMoveStrategy : MoveStrategy, IMovable
    {
        private Vector3 m_target;
        private float m_distance;

        public ParabolaMoveStrategy(Bullet bullet) : base(bullet)
        {
            this.SetTarget();
        }

        private void SetTarget()
        {
            Enemy closest = GameManager.Instance.GetEnemySpawnController().
                            GetClosestEnemy(m_bulletTransform.position);
            if(closest != null) m_target = closest.GetView().transform.position;
            else m_target = Utilities.RandomPosition();

            m_bullet.tower.towerView.SetTarget(m_target);

            m_target.z = Constants.BULLET_DEPTH;
            this.m_distance = Vector3.Distance(this.m_bulletTransform.position, this.m_target);
        }

        public void Move()
        {
            float currDistance = Vector3.Distance(m_bulletTransform.position, m_target);
            if(currDistance <= Constants.COLLIDE_OFFSET) return;
            m_bulletTransform.DOMove(m_target, 
                m_distance / m_bullet.speed.value / Constants.SPEED_MULTIPLIER).
                SetEase(Ease.OutQuad);
        }
    }

    public class CircleRoundMoveStrategy : MoveStrategy, IMovable
    {
        public CircleRoundMoveStrategy(Bullet bullet) : base(bullet)
        {
            
        }

        public void Move()
        {
            // don't move at all
        }
    }

    public class PlacementMoveStrategy : MoveStrategy, IMovable
    {
        public PlacementMoveStrategy(Bullet bullet) : base(bullet)
        {

        }

        public void Move()
        {
            
        }
    }
}