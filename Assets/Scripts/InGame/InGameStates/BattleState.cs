using UnityEngine;

using InGame.Cores;
using InGame.UI;

namespace InGame.InGameStates
{
    public class BattleState : InGameState
    {
        public override InGameStateType type => InGameStateType.BattleState;

        public override void Enter(InGameState last)
        {
            Debug.Log("Enter battle");
            GameManager.Instance.GetInputManager().RegisterClickEvent(OnClicked);

            BattleProgressUI.Instance.Show();
            BattleResultUI.Instance.Hide();
            ChariotStatusUI.Instance.Show();
            NavigationBarUI.Instance.Hide();
            BoardBarUI.Instance.Hide();

            // TODO minimise board view
        }
        
        public override void Exit()
        {
            Debug.Log("Exit battle");
            GameManager.Instance.GetInputManager().UnregisterClickEvent(OnClicked);

            BattleProgressUI.Instance.Hide();
            BattleResultUI.Instance.Show();
            ChariotStatusUI.Instance.Hide();
            NavigationBarUI.Instance.Show();
            BoardBarUI.Instance.Show();

            // TODO restore board view
        }
        
        private void OnClicked(Vector2 worldPosition)
        {

        }

        public static BattleState CreateState()
        {
            var state = new BattleState
            {

            };
            return state;
        }
    }
}