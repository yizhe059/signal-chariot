

namespace InGame.Boards.Modules.ModuleBuffs
{
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
                bulletSizePercentageBuff = bulletSizePercentageBuff
            };
        }

        

        public static WeaponBuff CreateBuff()
        {
            return new WeaponBuff();
        }

    }
}