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

            moveType = other.moveType;
            collisionEffects = other.collisionEffects;
            destructionEffects = other.destructionEffects;
        }
    }
}