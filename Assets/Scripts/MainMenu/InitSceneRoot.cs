using InGame;
using UnityEngine;
using Utils.Common;

namespace MainMenu
{
    public class InitSceneRoot : MonoBehaviour
    {
        public void OnTestPressed()
        {
            Game.Instance.nextState = new WorldState();
        }
    }
}
