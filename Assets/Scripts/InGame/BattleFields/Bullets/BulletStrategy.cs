using UnityEngine;
using DG.Tweening;

using Utils;
using InGame.BattleFields.Enemies;
using InGame.Cores;

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
    
    public interface IMoveStrategy
    {
        void Move();
    }

    public class LinearMoveStrategy : IMoveStrategy
    {
        private Bullet m_bullet;
        private Transform m_bulletTransform;
        private Vector3 m_direction;
    
        public LinearMoveStrategy(Bullet bullet)
        {
            this.m_bullet = bullet;
            this.m_bulletTransform = bullet.bulletView.transform;
            this.SetTarget();
        }

        private void SetTarget()
        {
            Enemy closest = GameManager.Instance.GetEnemySpawnController().
                            GetClosestEnemy(m_bulletTransform.position);
            if(closest != null) m_direction = closest.GetView().transform.position;
            else m_direction = Utilities.RandomPosition();

            m_bullet.tower.towerView.SetTarget(m_direction);

            m_direction = m_direction - m_bulletTransform.position;
            m_direction.z = Constants.BULLET_DEPTH;
            m_direction = m_direction.normalized *
                        Time.deltaTime * m_bullet.speed.value * 
                        Constants.SPEED_MULTIPLIER;
        }

        public void Move()
        {
            m_bulletTransform.Translate(m_direction, Space.World);
        }
    }

    public class FollowMoveStrategy : IMoveStrategy
    {
        private Bullet m_bullet;
        private Transform m_bulletTransform;
        private Transform m_target;

        public FollowMoveStrategy(Bullet bullet)
        {
            this.m_bullet = bullet;
            this.m_bulletTransform = bullet.bulletView.transform;
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
            direction = direction.normalized * 
                       Time.deltaTime * m_bullet.speed.value * 
                       Constants.SPEED_MULTIPLIER;
            m_bulletTransform.Translate(direction, Space.World);
        }
    }

    public class ParabolaMoveStrategy : IMoveStrategy
    {
        private Bullet m_bullet;
        private Transform m_bulletTransform;
        private Vector3 m_target;
        private float m_distance;

        public ParabolaMoveStrategy(Bullet bullet)
        {
            this.m_bullet = bullet;
            this.m_bulletTransform = bullet.bulletView.transform;
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

    public class CircleRoundMoveStrategy : IMoveStrategy
    {
        public void Move()
        {
            // don't move at all
        }
    }

    public class PlacementMoveStrategy : IMoveStrategy
    {
        public void Move()
        {
            
        }
    }

    public class RandomMoveStrategy : IMoveStrategy
    {  
        private Bullet m_bullet;
        private Transform m_bulletTransform;
        private Vector3 m_direction;

        public RandomMoveStrategy(Bullet bullet)
        {
            m_bullet = bullet;
            m_bulletTransform = m_bullet.bulletView.transform;
            m_direction = (Utilities.RandomPosition() - m_bulletTransform.position).normalized;
            m_direction *= Time.deltaTime * m_bullet.speed.value * 
                          Constants.SPEED_MULTIPLIER;
        }

        public void Move()
        {
            m_bulletTransform.Translate(m_direction, Space.World);
        }
    }
}