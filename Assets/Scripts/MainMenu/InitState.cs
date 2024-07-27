using UnityEngine.SceneManagement;
using Utils;
using Utils.Common;

namespace MainMenu
{
    public class InitState : GameState
    {
        public override void Enter(GameState last)
        {

            SceneManager.LoadScene(Constants.Main_MENU);
        }
    }
}