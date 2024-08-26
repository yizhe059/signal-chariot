using System;
using System.Collections.Generic;

using UnityEngine;
using InGame.BattleFields.Bullets;
using Editors.BattleEffects;

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
        public List<EffectEditor> collisionEffects = new();
        public List<EffectEditor> destructionEffects = new();

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