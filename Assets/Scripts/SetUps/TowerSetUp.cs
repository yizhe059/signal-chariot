using System;

using UnityEngine;

using InGame.BattleFields.Chariots;

namespace SetUps
{
    [Serializable]
    public class TowerSetUp
    {
        public Sprite sprite;
        public float damageMultipler;

        #region Bullet
        public BulletSetUp bulletSetUp;
        public int bulletCount;
        public float bulletShootInterval;
        public SeekMode seekMode;
        #endregion

        public TowerSetUp(TowerSetUp other)
        {
            sprite = other.sprite;
            damageMultipler = other.damageMultipler;

            bulletSetUp = new BulletSetUp(other.bulletSetUp);
            bulletCount = other.bulletCount;
            bulletShootInterval = other.bulletShootInterval;
            seekMode = other.seekMode;
        }
    }
}