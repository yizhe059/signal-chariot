using System;

using UnityEngine;
using InGame.BattleFields.Bullets;
using InGame.BattleEffects;
using System.Collections.Generic;

namespace SetUps
{
    [Serializable]
    public class BulletSetUp
    {
        public Sprite sprite;
        public float size;

        public float damage;
        public float health;
        public float speed;
        public float lifeTime;
        
        public int bouncingTimes; // TODO effect
        public int penetrateTimes; // TODO effect
        public int splitTimes; // TODO effect

        public MoveType moveType;
        public List<Effect> collisionEffects;
        public List<Effect> destructionEffects;

        public BulletSetUp(BulletSetUp other)
        {
            sprite = other.sprite;
            size = other.size;

            damage = other.damage;
            health = other.health;
            speed = other.speed;
            lifeTime = other.lifeTime;

            bouncingTimes = other.bouncingTimes; // TODO effect
            penetrateTimes = other.penetrateTimes; // TODO effect
            splitTimes = other.splitTimes; // TODO effect

            moveType = other.moveType;
            collisionEffects = other.collisionEffects;
            destructionEffects = other.destructionEffects;
        }
    }
}