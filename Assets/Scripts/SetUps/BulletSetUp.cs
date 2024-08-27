using System;
using System.Collections.Generic;

using UnityEngine;
using InGame.BattleFields.Bullets;
using InGame.BattleEffects;

namespace SetUps
{
    [Serializable]
    public class BulletSetUp
    {
        public string name;
        public Sprite sprite;
        
        public float size;
        public float damage;
        public float health;
        public float speed;
        public float lifeTime;

        public MoveType moveType;
        public List<Effect> collisionEffects = new();
        public List<Effect> destructionEffects = new();

        public BulletSetUp()
        {
            
        }
        
        public BulletSetUp(BulletSetUp other)
        {
            name = other.name;
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