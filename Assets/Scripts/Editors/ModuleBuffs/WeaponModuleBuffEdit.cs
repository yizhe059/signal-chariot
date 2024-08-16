using InGame.Boards.Modules.ModuleBuffs;
using UnityEngine.Serialization;

namespace Editors.ModuleBuffs
{
    public class WeaponModuleBuffEdit: BuffElementEdit
    {
        public int bouncingBuff = 0;
        public int splittingBuff = 0;
        public int penetrationBuff = 0;
        public int numShotsFlatBuff = 0;
        public int numBulletsPerShotFlatBuff = 0;
        public int speedPercentageBuff = 0;
        public int damagePercentageBuff = 0;
        public int flatDamageBuff = 0;
        public int bulletSizePercentageBuff = 0;
        public int lifeTimeBuff = 0;
        
        public override ModuleBuff CreateBuff()
        {
            return WeaponBuff.CreateBuff(bouncingBuff, splittingBuff, penetrationBuff, numShotsFlatBuff, numBulletsPerShotFlatBuff,
                speedPercentageBuff, damagePercentageBuff, flatDamageBuff, bulletSizePercentageBuff, lifeTimeBuff);
        }
    }
}