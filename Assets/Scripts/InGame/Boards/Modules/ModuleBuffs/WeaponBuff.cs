

namespace InGame.Boards.Modules.ModuleBuffs
{
    [System.Serializable]
    public class WeaponBuff: ModuleBuff
    {
        public override ModuleBuffType type => ModuleBuffType.Weapon;
        

        public int bouncingBuff = 0;
        public int splittingBuff = 0;
        public int penetrationBuff = 0;
        public int numBulletFlatBuff = 0;

        public int speedPercentageBuff = 0;
        public int damagePercentageBuff = 0;
        public int flatDamageBuff = 0;
        public int bulletSizePercentageBuff = 0;
        public int lifeTimeBuff = 0;
        
        protected override void OnAdd(ModuleBuff other)
        {
            WeaponBuff otherBuff = (WeaponBuff)other;

            bouncingBuff += otherBuff.bouncingBuff;
            splittingBuff += otherBuff.splittingBuff;
            penetrationBuff += otherBuff.penetrationBuff;
            numBulletFlatBuff += otherBuff.numBulletFlatBuff;
            speedPercentageBuff += otherBuff.speedPercentageBuff;
            damagePercentageBuff += otherBuff.damagePercentageBuff;
            flatDamageBuff += otherBuff.flatDamageBuff;
            bulletSizePercentageBuff += otherBuff.bulletSizePercentageBuff;
            lifeTimeBuff += otherBuff.lifeTimeBuff;
        }

        protected override void OnMinus(ModuleBuff other)
        {
            WeaponBuff otherBuff = (WeaponBuff)other;

            bouncingBuff -= otherBuff.bouncingBuff;
            splittingBuff -= otherBuff.splittingBuff;
            penetrationBuff -= otherBuff.penetrationBuff;
            numBulletFlatBuff -= otherBuff.numBulletFlatBuff;
            speedPercentageBuff -= otherBuff.speedPercentageBuff;
            damagePercentageBuff -= otherBuff.damagePercentageBuff;
            flatDamageBuff -= otherBuff.flatDamageBuff;
            bulletSizePercentageBuff -= otherBuff.bulletSizePercentageBuff;
            lifeTimeBuff -= otherBuff.lifeTimeBuff;
        }

        public override void SetDefault()
        {
            bouncingBuff = 0;
            splittingBuff = 0;
            penetrationBuff = 0;
            numBulletFlatBuff = 0;
            speedPercentageBuff = 0;
            damagePercentageBuff = 0;
            flatDamageBuff = 0;
            bulletSizePercentageBuff = 0;
            lifeTimeBuff = 0;
        }

        public override ModuleBuff CreateCopy()
        {
            return new WeaponBuff
            {
                bouncingBuff = bouncingBuff,
                splittingBuff = splittingBuff,
                penetrationBuff = penetrationBuff,
                numBulletFlatBuff = numBulletFlatBuff,
                speedPercentageBuff = speedPercentageBuff,
                damagePercentageBuff = damagePercentageBuff,
                flatDamageBuff = flatDamageBuff,
                bulletSizePercentageBuff = bulletSizePercentageBuff,
                lifeTimeBuff = lifeTimeBuff
            };
        }

        public override string ToString()
        {
            return $"bouncingBuff: {bouncingBuff}, splittingBuff: {splittingBuff}, penetrationBuff, {penetrationBuff}, " +
                   $"numBulletFlatBuff: {numBulletFlatBuff}, speedPercentageBuff: {speedPercentageBuff}, " +
                   $"damagePercentageBuff: {damagePercentageBuff}, flatDamageBuff: {flatDamageBuff}, " +
                   $"bulletSizePercentageBuff: {bulletSizePercentageBuff}, lifeTimeBuff: {lifeTimeBuff}";
        }

        public static WeaponBuff CreateBuff()
        {
            return new WeaponBuff();
        }
        
        public static WeaponBuff CreateBuff(int bouncingBuff, int splittingBuff, int penetrationBuff, int numBulletFlatBuff, 
            int speedPercentageBuff, int damagePercentageBuff, int flatDamageBuff, int bulletSizePercentageBuff, int lifeTimeBuff)
        {
            return new WeaponBuff
            {
                bouncingBuff = bouncingBuff,
                splittingBuff = splittingBuff,
                penetrationBuff = penetrationBuff,
                numBulletFlatBuff = numBulletFlatBuff,
                speedPercentageBuff = speedPercentageBuff,
                damagePercentageBuff = damagePercentageBuff,
                flatDamageBuff = flatDamageBuff,
                bulletSizePercentageBuff = bulletSizePercentageBuff,
                lifeTimeBuff = lifeTimeBuff
            };
        }
        
        

    }
}