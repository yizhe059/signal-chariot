using InGame.Boards.Modules.ModuleBuffs;
using InGame.Effects;
using InGame.Effects.EffectElement.ApplyModuleBuffEffects;

namespace Editors.Effects.ApplyBuffEffects
{
    public class ApplyMagazineBuffEdit: ApplyModuleBuffEffectEdit
    {
        public MagazineBuff buff;
        public override Effect CreateEffect()
        {
            return ApplyMagazineBuff.CreateEffect(range, (MagazineBuff) buff.CreateCopy());
        }
    }
}