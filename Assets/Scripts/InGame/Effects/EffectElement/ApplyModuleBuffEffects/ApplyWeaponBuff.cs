using InGame.Boards.Modules.ModuleBuffs;

namespace InGame.Effects.EffectElement.ApplyModuleBuffEffects
{
    public class ApplyWeaponBuff: ApplyModuleBuff
    {
        public WeaponBuff buff;
        
        public override ModuleBuff GetBuff()
        {
            return buff.CreateCopy();
        }

        public override ApplyModuleBuff OnCopy()
        {
            return new ApplyWeaponBuff
            {
                buff = (WeaponBuff) buff.CreateCopy(),
            };
        }

        public static ApplyWeaponBuff CreateEffect(BuffRange range, WeaponBuff buff)
        {
            return new ApplyWeaponBuff
            {
                range = range,
                buff = (WeaponBuff) buff.CreateCopy()
            };
        }
    }
}