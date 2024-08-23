namespace InGame.Effects.EffectElement
{
    public class DestroySignalEffect: Effect
    {
        protected override bool canEffectByTest => true;
        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            if (blackBoard.signal == null) return;
            
            blackBoard.signal.ConsumeEnergy(blackBoard.signal.energy);
        }
        
        // To Do: No need to create a copy if it does not have any field
        public override Effect CreateCopy()
        {
            return new DestroySignalEffect();
        }

        public static Effect CreateEffect()
        {
            return new DestroySignalEffect();
        }
    }
}