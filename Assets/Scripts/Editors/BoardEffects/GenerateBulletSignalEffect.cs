using InGame.Boards.Signals;
using InGame.Effects;
using UnityEngine;

namespace Editors.Effects.CustomEffectTrigger
{
    public class GenerateBulletSignalEffect: EffectEdit
    {
        public SignalType type;
        [Min(1)]
        public int numOfBullets;
        public override Effect CreateEffect()
        {
            return InGame.Effects.EffectElement.GenerateBulletSignalEffect.CreateEffect(type, numOfBullets);
        }
    }
}