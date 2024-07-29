using InGame.Boards.Signals;


namespace InGame.Effects
{
    public class SignalDirectionChangeEffect: Effect
    {
        protected override bool canEffectByTest => true;

        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            
            // new direction only depend on the orientation of the module
            var newDir = Signal.OrientationToDirection(m_module.orientation, Signal.Direction.Up);
            blackBoard.signal.SetDirection(newDir);
        }

        public override Effect CreateCopy()
        {
            return new SignalDirectionChangeEffect();
        }

        public static SignalDirectionChangeEffect CreateEffect()
        {
            return new SignalDirectionChangeEffect();
            
        }
    }
}