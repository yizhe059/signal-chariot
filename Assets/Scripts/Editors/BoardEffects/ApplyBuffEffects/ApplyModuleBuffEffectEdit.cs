using InGame.Effects;
using InGame.Effects.EffectElement.ApplyModuleBuffEffects;

namespace Editors.Effects.ApplyBuffEffects
{
    public abstract class ApplyModuleBuffEffectEdit: EffectEdit
    {
        public BuffRange range;

        public abstract override Effect CreateEffect();
    }
}