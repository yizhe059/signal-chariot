using UnityEngine;

using InGame.BattleFields.Common;
using InGame.Views;
using SetUps;
using Utils;
using InGame.Cores;
using Unity.VisualScripting;
using Utils.Common;
using System;
using InGame.BattleFields.Androids;

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

        [Header("Properties")]
        private Sprite m_sprite;
        private UnlimitedProperty m_size;
        private IMoveStrategy m_moveStrategy;
        private DamageType m_damageStrategy;
        private UnlimitedProperty m_speed;
        private UnlimitedProperty m_damage;
        private UnlimitedProperty m_lifeTime;
        private UnlimitedProperty m_bouncingTimes;
        private UnlimitedProperty m_penetrateTimes;
        private UnlimitedProperty m_splitTimes;
        
        // TODO DieEffect

        public Sprite sprite{ get { return m_sprite;}}
        public UnlimitedProperty size{ get { return m_size;}}
        public IMoveStrategy moveStrategy { get { return m_moveStrategy;}}
        public DamageType damageType {get { return m_damageStrategy;}}
        public UnlimitedProperty speed { get { return m_speed;}}
        public UnlimitedProperty damage { get { return m_damage;}}
        public UnlimitedProperty lifetime { get { return m_lifeTime;}}
        public UnlimitedProperty bouncingTimes { get { return m_bouncingTimes;}}
        public UnlimitedProperty penetrateTimes { get {return m_penetrateTimes;}}
        public UnlimitedProperty splitTimes { get {return m_splitTimes;}}

        public Bullet(BulletSetUp bulletSetUp, Tower tower)
        {
            m_tower = tower;
            
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
            CreateDamageStrategy(bulletSetUp.damageType);
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
                    m_moveStrategy = new CircleRoundMoveStrategy();
                    break;
                case MoveType.Placement:
                    m_moveStrategy = new PlacementMoveStrategy();
                    break;
                case MoveType.Random:
                    m_moveStrategy = new RandomMoveStrategy(this);
                    break;
                default:
                    Debug.LogWarning("No matching move type: " + moveType);
                    break;
            }
        }

        private void CreateDamageStrategy(DamageType damageType)
        {
            switch(damageType)
            {
                case DamageType.Collide:
                    break;
                case DamageType.Range:
                    break;
                default:
                    Debug.LogError("No matching damage type: " + damageType);
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
            // TODO
            if(m_bouncingTimes.value > 0) m_moveStrategy = new LinearMoveStrategy(this);
            else this.Die();
        }
    }
}