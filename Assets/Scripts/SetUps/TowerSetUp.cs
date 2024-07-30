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
        public SeekMode seekMode;
        #endregion
    }
}