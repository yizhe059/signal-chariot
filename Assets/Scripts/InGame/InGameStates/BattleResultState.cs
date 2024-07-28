using UnityEngine;

using InGame.Cores;
using InGame.UI;

namespace InGame.InGameStates
{
    public class BattleResultState : InGameState
    {
        public override InGameStateType type => InGameStateType.BattleResultState;

        public override void Enter(InGameState last)
        {
            Debug.Log("Enter battle result");

            BattleProgressUI.Instance.Show();
            BattleResultUI.Instance.Show();
            ChariotStatusUI.Instance.Show();
            NavigationBarUI.Instance.Hide();
            BoardBarUI.Instance.Hide();

            // TODO minimise board view
        }
        
        public override void Exit()
        {
            Debug.Log("Exit battle result");
            BattleProgressUI.Instance.Hide();
            BattleResultUI.Instance.Hide();
            ChariotStatusUI.Instance.Hide();
            NavigationBarUI.Instance.Show();
            BoardBarUI.Instance.Show();

            // TODO restore board view
        }

        public static BattleResultState CreateState()
        {
            var state = new BattleResultState
            {

            };
            return state;
        }
    }
}