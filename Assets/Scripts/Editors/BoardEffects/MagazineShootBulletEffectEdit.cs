using InGame.Effects;
using InGame.Effects.EffectElement;
using UnityEngine;

namespace Editors.Effects
{
    public class MagazineShootBulletEffectEdit: EffectEdit
    {
        [Min(0)]
        public int magazineCapacity;
        
        public override Effect CreateEffect()
        {
            return MagazineShootBulletEffect.CreateEffect(magazineCapacity);
        }
    }
}