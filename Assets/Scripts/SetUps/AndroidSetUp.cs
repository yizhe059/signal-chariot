using System;
using InGame.BattleFields.Androids;
using InGame.BattleFields.Common;

namespace SetUps
{   
    [Serializable]
    public class AndroidSetUp
    {   
        #region Health
        public float initialHealth = 50;
        public float maxHealth = 100;
        #endregion

        #region Shield
        public float defense = 0;
        public int armor = 0;
        #endregion

        public float speed = 1f;

        #region Resources
        public int initialMod = 0;
        public int maxMod = 500;
        public int initialCrystal = 0;
        public int maxCrystal = 50;
        #endregion

        public AndroidSetUp(Android android)
        {
            initialHealth = android.Get(LimitedPropertyType.Health, true);
            maxHealth = android.Get(LimitedPropertyType.Health, false);
        }

        public void SetAndroid(Android android)
        {
            android.Set(LimitedPropertyType.Health, maxHealth, false);
            android.Set(LimitedPropertyType.Health, initialHealth, true);
        }
    }
}