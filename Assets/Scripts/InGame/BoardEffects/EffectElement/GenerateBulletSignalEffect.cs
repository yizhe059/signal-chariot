using InGame.Boards.Signals;
using InGame.Cores;
using SetUps;

namespace InGame.Effects.EffectElement
{
    public class GenerateBulletSignalEffect: Effect
    {
        public SignalType type;
        public int numOfBullets;
        
        public override void OnTrigger(EffectBlackBoard blackBoard)
        {
            var signalController = GameManager.Instance.GetSignalController();
            var orientation = m_module.orientation;
            signalController.CreateSignal(new SignalSetUp
            {
                dir = Signal.OrientationToDirection(orientation, Signal.Direction.Up),
                energy = numOfBullets,
                pos = blackBoard.slot.pos
            }, 0, type);
        }

        public override Effect CreateCopy()
        {
            return new GenerateBulletSignalEffect
            {
                type = type,
                numOfBullets = numOfBullets
            };
        }

        public static Effect CreateEffect(SignalType type, int numOfBullets)
        {
            return new GenerateBulletSignalEffect
            {
                type = type,
                numOfBullets = numOfBullets
            };
            
        }
        
        
    }
}