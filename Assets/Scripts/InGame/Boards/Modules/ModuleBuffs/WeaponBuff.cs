namespace InGame.Boards.Modules.ModuleBuffs
{
    [System.Serializable]
    public class WeaponBuff: ModuleBuff
    {
        public override ModuleBuffType type => ModuleBuffType.Weapon;
        
        public int bouncingBuff = 0;
        public int splittingBuff = 0;
        public int penetrationBuff = 0; 
        public int numShotsFlatBuff = 0;
        public int numBulletsPerShotFlatBuff = 0;
        public int speedPercentageBuff = 0; 
        public int damagePercentageBuff = 0;
        public int flatDamageBuff = 0;
        public int bulletSizePercentageBuff = 0;
        public int lifeTimePercentageBuff = 0;
        
        protected override void OnAdd(ModuleBuff other)
        {
            WeaponBuff otherBuff = (WeaponBuff)other;

            bouncingBuff += otherBuff.bouncingBuff;
            splittingBuff += otherBuff.splittingBuff;
            penetrationBuff += otherBuff.penetrationBuff;
            numShotsFlatBuff += otherBuff.numShotsFlatBuff;
            speedPercentageBuff += otherBuff.speedPercentageBuff;
            damagePercentageBuff += otherBuff.damagePercentageBuff;
            flatDamageBuff += otherBuff.flatDamageBuff;
            bulletSizePercentageBuff += otherBuff.bulletSizePercentageBuff;
            lifeTimePercentageBuff += otherBuff.lifeTimePercentageBuff;
            numBulletsPerShotFlatBuff += otherBuff.numBulletsPerShotFlatBuff;
        }

        protected override void OnMinus(ModuleBuff other)
        {
            WeaponBuff otherBuff = (WeaponBuff)other;

            bouncingBuff -= otherBuff.bouncingBuff;
            splittingBuff -= otherBuff.splittingBuff;
            penetrationBuff -= otherBuff.penetrationBuff;
            numShotsFlatBuff -= otherBuff.numShotsFlatBuff;
            speedPercentageBuff -= otherBuff.speedPercentageBuff;
            damagePercentageBuff -= otherBuff.damagePercentageBuff;
            flatDamageBuff -= otherBuff.flatDamageBuff;
            bulletSizePercentageBuff -= otherBuff.bulletSizePercentageBuff;
            lifeTimePercentageBuff -= otherBuff.lifeTimePercentageBuff;
            numBulletsPerShotFlatBuff -= otherBuff.numBulletsPerShotFlatBuff;
        }

        public override void SetDefault()
        {
            bouncingBuff = 0;
            splittingBuff = 0;
            penetrationBuff = 0;
            numShotsFlatBuff = 0;
            speedPercentageBuff = 0;
            damagePercentageBuff = 0;
            flatDamageBuff = 0;
            bulletSizePercentageBuff = 0;
            lifeTimePercentageBuff = 0;
            numBulletsPerShotFlatBuff = 0;
        }

        public override ModuleBuff CreateCopy()
        {
            return new WeaponBuff
            {
                bouncingBuff = bouncingBuff,
                splittingBuff = splittingBuff,
                penetrationBuff = penetrationBuff,
                numShotsFlatBuff = numShotsFlatBuff,
                speedPercentageBuff = speedPercentageBuff,
                damagePercentageBuff = damagePercentageBuff,
                flatDamageBuff = flatDamageBuff,
                bulletSizePercentageBuff = bulletSizePercentageBuff,
                lifeTimePercentageBuff = lifeTimePercentageBuff,
                numBulletsPerShotFlatBuff = numBulletsPerShotFlatBuff
            };
        }

        public override string ToString()
        {
            return $"bouncingBuff: {bouncingBuff}, splittingBuff: {splittingBuff}, penetrationBuff, {penetrationBuff}, " +
                   $"numShotsFlatBuff: {numShotsFlatBuff}, numBulletsPerShotFlatBuff: {numBulletsPerShotFlatBuff}," +
                   $" speedPercentageBuff: {speedPercentageBuff}, " +
                   $"damagePercentageBuff: {damagePercentageBuff}, flatDamageBuff: {flatDamageBuff}, " +
                   $"bulletSizePercentageBuff: {bulletSizePercentageBuff}, lifeTimePercentageBuff: {lifeTimePercentageBuff}";
        }

        public static WeaponBuff CreateBuff()
        {
            return new WeaponBuff();
        }
        
        public static WeaponBuff CreateBuff(int bouncingBuff, int splittingBuff, int penetrationBuff, int numShotsFlatBuff,
            int numBulletsPerShotFlatBuff, int speedPercentageBuff, int damagePercentageBuff, int flatDamageBuff, 
            int bulletSizePercentageBuff, int lifeTimePercentageBuff)
        {
            return new WeaponBuff
            {
                bouncingBuff = bouncingBuff,
                splittingBuff = splittingBuff,
                penetrationBuff = penetrationBuff,
                numShotsFlatBuff = numShotsFlatBuff,
                numBulletsPerShotFlatBuff = numBulletsPerShotFlatBuff,
                speedPercentageBuff = speedPercentageBuff,
                damagePercentageBuff = damagePercentageBuff,
                flatDamageBuff = flatDamageBuff,
                bulletSizePercentageBuff = bulletSizePercentageBuff,
                lifeTimePercentageBuff = lifeTimePercentageBuff
            };
        }
        
        

    }
}