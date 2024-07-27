using InGame.Boards.Signals;
using InGame.Effects;

namespace Editors.Effects
{
    public class SignalActiveGenerationEffectEdit: EffectEdit
    {
        public float coolDown;
        public int signalStrength;
        public Signal.Direction dir;
        public override Effect CreateEffect()
        {
            return SignalActiveGenerationEffect.CreateEffect(coolDown, signalStrength, dir);
        }
    }
}