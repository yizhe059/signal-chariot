using System;

namespace SetUps
{   
    [Serializable]
    public class ChariotSetUp
    {
        public float initialHealth = 50;
        public float maxHealth = 100;

        public float armor = 0;

        public float speed = 1f;
    }
}