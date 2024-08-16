using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Utils.Common;

namespace MainMenu
{
    public class InitState : GameState
    {
        public InitState(){}
        
        public override void Enter(GameState last)
        {
            SceneManager.LoadScene(Constants.MAIN_MENU);
        }
    }
}