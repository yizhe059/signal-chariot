using UnityEngine;
using Utils.Common;
using World.Views;

namespace World.Cores
{
    public class GameManager: MonoSingleton<GameManager>
    {
        [SerializeField] private SetUp m_setUp;
        private Board m_board;

        [SerializeField]
        private BoardView m_boardView;

        protected override void Init()
        {
            m_board = new Board(m_setUp.boardSetUp);

            m_boardView.Init(m_board, m_setUp.boardSetUp);
            Debug.Log(m_board);
        }
    }
}