using UnityEngine;

using Utils;
using Utils.Common;

using InGame.Cores;
using InGame.BattleFields.Enemies;


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

            m_bullet.tower.towerView.SetTarget(target);

            Vector3 direction = target - m_bulletTransform.position;
            direction.z = Constants.BULLET_DEPTH;

            m_bulletManager.SetBatchInfo(direction, m_batchIdx);
        }

        private void SetDirection()
        {
            Vector3 direction = m_bulletManager.GetBatchInfo(m_batchIdx);

            int batchSize = m_bulletManager.GetBatchSize(m_batchIdx);
            float medium = (batchSize%2 == 0) ? (batchSize/2 - 0.5f) : batchSize/2;
            float theta = (m_bulletIdx - medium) * Constants.BULLET_BATCH_ROTATION_DEGREE * Mathf.Deg2Rad;

            Vector3 currDirection;
            currDirection.x = Mathf.Cos(theta) * direction.x - Mathf.Sin(theta) * direction.y;
            currDirection.y = Mathf.Sin(theta) * direction.x + Mathf.Cos(theta) * direction.y;
            currDirection.z = direction.z;

            // m_bulletTransform.rotation = Quaternion.LookRotation(currDirection);
            m_velocity = Constants.SPEED_MULTIPLIER * m_bullet.speed.value * 
                        Time.deltaTime * currDirection.normalized;
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
            Enemy random = GameManager.Instance.GetEnemySpawnController().GetRandomEnemy();
            Vector3 target;
            if(random != null) target = random.GetView().transform.position;
            else target = Utilities.RandomPosition();

            m_bullet.tower.towerView.SetTarget(target);
            
            target.z = Constants.BULLET_DEPTH;
            m_bulletManager.SetBatchInfo(target, m_batchIdx);
        }

        private void SetDirection()
        {
            Vector3 target = m_bulletManager.GetBatchInfo(m_batchIdx);
            Vector3 direction = (target - m_bulletTransform.position).normalized;
            // m_bulletTransform.rotation = Quaternion.LookRotation(direction);
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

        protected override void SetBatchInfo()
        {

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
            // m_bulletTransform.rotation = Quaternion.LookRotation(direction);
            Vector3 velocity = Constants.SPEED_MULTIPLIER * m_bullet.speed.value * 
                                Time.deltaTime * direction.normalized;
            m_bulletTransform.Translate(velocity, Space.World);
        }
    }

    public class ParabolaMoveStrategy : MoveStrategy, IMovable
    {
        private CountdownTimer m_timer;

        public ParabolaMoveStrategy(Bullet bullet) : base(bullet)
        {
            this.SetTarget();
            this.m_bullet.bulletView.Disable();
        }

        protected override void SetBatchInfo()
        {
            Enemy random = GameManager.Instance.GetEnemySpawnController().GetRandomEnemy();
            Vector3 target;
            if(random != null) target = random.GetView().transform.position;
            else target = Utilities.RandomPosition();  
            
            m_bullet.tower.towerView.SetTarget(target);
            
            target.z = Constants.BULLET_DEPTH;
            m_bulletManager.SetBatchInfo(target, m_batchIdx);
        }

        private void SetTarget()
        {
            Vector3 target = m_bulletManager.GetBatchInfo(m_batchIdx);
            float distance = Vector3.Distance(this.m_bulletTransform.position, target);
            
            m_timer = new CountdownTimer(
                distance / m_bullet.speed.value / Constants.SPEED_MULTIPLIER
            );

            m_timer.OnTimerComplete.AddListener(() => {
                m_bullet.bulletView.Enable();
                m_bulletTransform.position = target;
            });

            m_timer.StartTimer();
        }

        public void Move()
        {
            m_timer.Update(Time.deltaTime);   
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