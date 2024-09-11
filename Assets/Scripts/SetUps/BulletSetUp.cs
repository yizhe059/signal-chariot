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
        public BulletType type;
        public int level;

        public Sprite sprite;
        public float size;
        public float damage;
        public float health;
        public float speed;
        public float lifeTime;

        public MoveType moveType;
        
        [SerializeReference] public List<Effect> collisionEffects;
        [SerializeReference] public List<Effect> destructionEffects;

        public BulletSetUp()
        {
            collisionEffects = new();
            destructionEffects = new();
        }
        
        public BulletSetUp(BulletSetUp other)
        {
            name = other.name;
            type = other.type;
            level = other.level;

            sprite = other.sprite;
            size = other.size;
            damage = other.damage;
            health = other.health;
            speed = other.speed;
            lifeTime = other.lifeTime;

            moveType = other.moveType;

            collisionEffects = new List<Effect>();
            foreach (var effect in other.collisionEffects)
            {
                collisionEffects.Add(effect.CreateCopy());
            }

            destructionEffects = new List<Effect>();
            foreach (var effect in other.destructionEffects)
            {
                destructionEffects.Add(effect.CreateCopy());
            }
        }
    }
}