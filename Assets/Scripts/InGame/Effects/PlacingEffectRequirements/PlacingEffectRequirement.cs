using InGame.Boards;

namespace InGame.Effects.PlacingEffectRequirements
{
    public abstract class PlacingEffectRequirement
    {
        public abstract bool CanTrigger(Slot slot);
    }
}