using System;

namespace SetUps
{   
    [Serializable]
    public class AndroidSetUp
    {   
        #region Health
        public float initialHealth = 50;
        public float maxHealth = 100;
        #endregion

        public float defence = 0;
        
        public float speed = 1f;

        public int mod = 0;
    }
}