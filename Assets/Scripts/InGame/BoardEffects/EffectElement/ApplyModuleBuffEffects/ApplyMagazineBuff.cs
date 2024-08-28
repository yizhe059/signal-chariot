using InGame.Boards.Modules.ModuleBuffs;

namespace InGame.Effects.EffectElement.ApplyModuleBuffEffects
{
    public class ApplyMagazineBuff: ApplyModuleBuff
    {
        public MagazineBuff buff;
        public override ModuleBuff GetBuff()
        {
            return buff.CreateCopy();
        }

        public override ApplyModuleBuff OnCopy()
        {
            return new ApplyMagazineBuff
            {
                buff = (MagazineBuff) buff.CreateCopy()
            };
        }
        
        public static ApplyMagazineBuff CreateEffect(BuffRange range, MagazineBuff buff)
        {
            return new ApplyMagazineBuff
            {
                range = range,
                buff = (MagazineBuff) buff.CreateCopy()
            };
        }
    }
}