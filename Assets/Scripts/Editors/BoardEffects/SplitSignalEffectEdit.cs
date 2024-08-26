using System.Collections.Generic;
using InGame.Boards.Signals;
using InGame.Effects;
using InGame.Effects.EffectElement;

namespace Editors.Effects
{
    public class SplitSignalEffectEdit: EffectEdit
    {
        public List<Signal.Direction> splittingDirs;
        
        public override Effect CreateEffect()
        {
            return SplitSignalEffect.CreateEffect(new List<Signal.Direction>(splittingDirs));
        }
    }
}