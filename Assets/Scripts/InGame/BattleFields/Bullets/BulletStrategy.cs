using UnityEngine;

namespace InGame.BattleFields.Bullets
{
    public enum MoveType
    {
        Linear,
        Follow,
        Parabola,
        CircleRound,
        Placement,
    }
    public enum DamageType
    {
        Collide,
        Range,
    }
    
    public interface IMoveStrategy
    {
        void Move(Bullet bullet);
    }

    public class LinearMoveStrategy : IMoveStrategy
    {
        private Vector3 direction;
    
        public LinearMoveStrategy(Vector3 direction)
        {
            this.direction = direction;
        }

        public void Move(Bullet bullet)
        {
            // bullet.Position += direction * bullet.Speed * Time.deltaTime;
        }
    }

    public class FollowMoveStrategy : IMoveStrategy
    {
        private Transform target;

        public FollowMoveStrategy(Transform target)
        {
            this.target = target;
        }

        public void Move(Bullet bullet)
        {
            // Vector3 direction = (target.position - bullet.Position).normalized;
            // bullet.Position += direction * bullet.Speed * Time.deltaTime;
        }
    }

    public class ParabolaMoveStrategy : IMoveStrategy
    {
        private Transform target;
        private AnimationCurve curve;

        public ParabolaMoveStrategy(Transform target, AnimationCurve curve)
        {
            this.target = target;
            this.curve = curve;
        }

        public void Move(Bullet bullet)
        {
            // float t = bullet.TimeAlive / bullet.TotalTime;
            // Vector3 direction = (target.position - bullet.Position).normalized;
            // bullet.Position += direction * bullet.Speed * curve.Evaluate(t) * Time.deltaTime;
        }
    }
}