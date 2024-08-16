using System.Collections.Generic;
using InGame.Boards;
using InGame.Boards.Modules;
using InGame.Cores;
using UnityEngine;

namespace InGame.InGameStates
{
    public class RewardState: InGameState
    {
        private bool m_isLastWave;
        private List<int> m_rewards;
        private GeneralBoard m_generalBoard;
        private ModuleLib m_moduleLib;
        
        public override InGameStateType type => InGameStateType.RewardState;

        public override void Enter(InGameState last)
        {
            Debug.Log("Enter Reward State");
            foreach (var moduleID in m_rewards)
            {
                Module module = m_moduleLib.GenerateModule(moduleID);

                if (!m_generalBoard.AddModule(module,out var pos))
                {
                    Debug.LogError("NO ROOM! I don't know what to do, I will just panic");
                    return;
                }
                
            }

            if (m_isLastWave)
            {
                GameManager.Instance.ChangeToBattleResultState(BattleResultType.BattleWin);
            }
            else
            {
                GameManager.Instance.ChangeToBattleResultState(BattleResultType.WaveWin);
            }
        }

        public override void Exit()
        {
            Debug.Log("Exit Reward State");
        }

        public static RewardState CreateState(bool isLastWave, List<int> rewards, GeneralBoard generalBoard,
            ModuleLib moduleLib)
        {
            var state = new RewardState
            {
                m_isLastWave = isLastWave,
                m_rewards = rewards,
                m_generalBoard = generalBoard,
                m_moduleLib = moduleLib
            };
            return state;
        }
    }
}