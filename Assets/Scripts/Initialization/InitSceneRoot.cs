using UnityEngine; 
using Utils.Common;
using World;

namespace Initialization
{
    public class InitSceneRoot : MonoBehaviour
    {
        public void OnTestPressed()
        {
            Game.Instance.nextState = new WorldState();
        }
    }
}
