using System.Collections.Generic;

using UnityEngine;

using SetUps;

using InGame.Cores;
using InGame.Views;
using InGame.BattleFields.Androids;
using InGame.BattleFields.Common;
using InGame.BattleEffects;

using Utils;
using Utils.Common;

namespace InGame.BattleFields.Bullets
{
    public class Bullet
    {   
        [Header("View")]
        private BulletView m_bulletView;
        public BulletView bulletView {get { return m_bulletView;}}

        [Header("Generator")]
        private Tower m_tower;
        public Tower tower {get { return m_tower;}}
        private int[] m_bulletIdx;
        public int[] bulletIdx {get { return m_bulletIdx;}}

        [Header("Properties")]
        private Sprite m_sprite;
        private UnlimitedProperty m_size;
        private UnlimitedProperty m_speed;
        private UnlimitedProperty m_damage;
        private UnlimitedProperty m_lifeTime;
        private UnlimitedProperty m_bouncingTimes; // TODO effect
        private UnlimitedProperty m_penetrateTimes; // TODO effect
        private UnlimitedProperty m_splitTimes; // TODO effect

        [Header("Logics")]
        private IMovable m_moveStrategy;
        private List<Effect> m_collisionEffects;
        private List<Effect> m_destructionEffects;

        public Sprite sprite{ get { return m_sprite;}}
        public UnlimitedProperty size{ get { return m_size;}}
        public UnlimitedProperty speed { get { return m_speed;}}
        public UnlimitedProperty damage { get { return m_damage;}}
        public UnlimitedProperty lifetime { get { return m_lifeTime;}}
        public UnlimitedProperty bouncingTimes { get { return m_bouncingTimes;}} // TODO effect
        public UnlimitedProperty penetrateTimes { get {return m_penetrateTimes;}} // TODO effect
        public UnlimitedProperty splitTimes { get {return m_splitTimes;}} // TODO effect
        public IMovable moveStrategy { get { return m_moveStrategy;}}

        public Bullet(BulletSetUp bulletSetUp, Tower tower, int[] bulletIdx)
        {
            m_tower = tower;
            m_bulletIdx = bulletIdx;
            
            UnlimitedProperty dmg = new(bulletSetUp.damage, UnlimitedPropertyType.Damage);
            UnlimitedProperty spd = new(bulletSetUp.speed, UnlimitedPropertyType.Speed);
            UnlimitedProperty lft = new(bulletSetUp.lifeTime);
            UnlimitedProperty rfl = new(bulletSetUp.bouncingTimes);
            UnlimitedProperty pnt = new(bulletSetUp.penetrateTimes);
            UnlimitedProperty spl = new(bulletSetUp.splitTimes);
            UnlimitedProperty siz = new(bulletSetUp.size * Constants.BULLET_SIZE_MULTIPLIER);

            m_damage = dmg;
            m_speed = spd;
            m_lifeTime = lft;
            m_bouncingTimes = rfl;
            m_penetrateTimes = pnt;
            m_splitTimes = spl;

            m_size = siz;
            m_sprite = bulletSetUp.sprite;
            CreateView();

            CreateMoveStrategy(bulletSetUp.moveType);
            CreateEffects();
        }

        private void CreateEffects()
        {
            m_collisionEffects = new();
            m_destructionEffects = new();
        }

        private void CreateView()
        {
            GameObject bulletPref = Resources.Load<GameObject>(Constants.GO_BULLET_PATH);
            GameObject bulletGO = GameObject.Instantiate(bulletPref);
            
            float x = GameManager.Instance.GetAndroid().androidView.transform.position.x;
            float y = GameManager.Instance.GetAndroid().androidView.transform.position.y;

            bulletGO.transform.position = new(x, y, Constants.BULLET_DEPTH);

            m_bulletView = bulletGO.GetComponent<BulletView>();
            m_bulletView.Init(this);
        }

        private void CreateMoveStrategy(MoveType moveType)
        {
            switch (moveType)
            {
                case MoveType.Linear:
                    m_moveStrategy = new LinearMoveStrategy(this);
                    break;
                case MoveType.Follow:
                    m_moveStrategy = new FollowMoveStrategy(this);
                    break;
                case MoveType.Parabola:
                    m_moveStrategy = new ParabolaMoveStrategy(this);
                    break;
                case MoveType.CircleRound:
                    m_moveStrategy = new CircleRoundMoveStrategy(this);
                    break;
                case MoveType.Placement:
                    m_moveStrategy = new PlacementMoveStrategy(this);
                    break;
                case MoveType.Random:
                    m_moveStrategy = new RandomMoveStrategy(this);
                    break;
                default:
                    Debug.LogWarning("No matching move type: " + moveType);
                    break;
            }
        }

        public void Die()
        {
            m_bulletView.Die();
        }

        public void DealDamage(IDamageable target, float dmg)
        {
            target.TakeDamage(dmg);
            
            if(m_bouncingTimes.value > 0){
                m_moveStrategy = new LinearMoveStrategy(this); // TODO effect
                m_bouncingTimes.value--;
            } 
            else this.Die();
        }
    }
}