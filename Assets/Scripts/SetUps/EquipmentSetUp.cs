using System;

using UnityEngine;

using InGame.BattleFields.Bullets;

namespace SetUps
{
    [Serializable]
    public class EquipmentSetUp
    {
        public Sprite sprite;
        // public float damageMultipler;
        public float seekInterval;
        public float shootInterval;
        public int shootCount;
        public int bulletCount;

        public EquipmentSetUp(EquipmentSetUp other)
        {
            sprite = other.sprite;
            // damageMultipler = other.damageMultipler;
            bulletCount = other.bulletCount;
            shootCount = other.shootCount;
            seekInterval = other.seekInterval;
            shootInterval = other.shootInterval;
        }
    }
}