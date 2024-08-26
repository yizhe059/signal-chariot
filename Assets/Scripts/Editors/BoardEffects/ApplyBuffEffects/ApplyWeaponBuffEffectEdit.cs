using InGame.Boards.Modules.ModuleBuffs;
using InGame.Effects;
using InGame.Effects.EffectElement.ApplyModuleBuffEffects;

namespace Editors.Effects.ApplyBuffEffects
{
    public class ApplyWeaponBuffEffectEdit: ApplyModuleBuffEffectEdit
    {
        public WeaponBuff buff;

        public override Effect CreateEffect()
        {
            return ApplyWeaponBuff.CreateEffect(range, (WeaponBuff) buff.CreateCopy());
        }
    }
}