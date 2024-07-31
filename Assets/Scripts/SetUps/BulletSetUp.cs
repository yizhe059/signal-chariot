using System;

using UnityEngine;

namespace SetUps
{
    [Serializable]
    public class BulletSetUp
    {
        public Sprite sprite;
        public float damage;
        public float speed;
        public float range;

        public BulletSetUp(BulletSetUp other)
        {
            sprite = other.sprite;
            damage = other.damage;
            speed = other.speed;
            range = other.range;
        }
    }
}