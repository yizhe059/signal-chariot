using UnityEngine.SceneManagement;
using Utils;
using Utils.Common;

namespace Initialization
{
    public class InitState : GameState
    {
        public override void Enter(GameState last)
        {

            SceneManager.LoadScene(Constants.Main_MENU);
        }
    }
}