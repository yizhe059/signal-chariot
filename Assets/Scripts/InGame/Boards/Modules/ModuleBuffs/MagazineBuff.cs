using UnityEngine.Serialization;

namespace InGame.Boards.Modules.ModuleBuffs
{
    [System.Serializable]
    public class MagazineBuff: ModuleBuff
    {
        public int magazineCapacityBuff = 0;
        
        public override ModuleBuffType type => ModuleBuffType.Magazine;
        public override ModuleBuff CreateCopy()
        {
            return new MagazineBuff
            {
                magazineCapacityBuff = this.magazineCapacityBuff
            };
        }

        protected override void OnAdd(ModuleBuff other)
        {
            var magazineBuff = (MagazineBuff)other;

            this.magazineCapacityBuff += magazineBuff.magazineCapacityBuff;
        }

        protected override void OnMinus(ModuleBuff other)
        {
            var magazineBuff = (MagazineBuff)other;

            this.magazineCapacityBuff -= magazineBuff.magazineCapacityBuff;
        }

        public override void SetDefault()
        {
            this.magazineCapacityBuff -= 0;
        }
        
        public static MagazineBuff CreateBuff()
        {
            return new MagazineBuff();
        }
        
        public static MagazineBuff CreateBuff(int capacityBuff)
        {
            return new MagazineBuff
            {
                magazineCapacityBuff = capacityBuff
            };
        }
    }
}