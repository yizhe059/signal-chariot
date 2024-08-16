using System;

using UnityEngine;
using InGame.BattleFields.Bullets;

namespace SetUps
{
    [Serializable]
    public class BulletSetUp
    {
        public Sprite sprite;

        public MoveType moveType;
        public DamageType damageType;
        
        public float damage;
        public float speed;
        public float lifeTime;
        
        public int reflectTimes;
        public int penetrateTimes;
        public int splitTimes;

        public BulletSetUp(BulletSetUp other)
        {
            sprite = other.sprite;
            
            moveType = other.moveType;
            damageType = other.damageType;

            damage = other.damage;
            speed = other.speed;
            lifeTime = other.lifeTime;

            reflectTimes = other.reflectTimes;
            penetrateTimes = other.penetrateTimes;
            splitTimes = other.splitTimes;
        }
    }
}