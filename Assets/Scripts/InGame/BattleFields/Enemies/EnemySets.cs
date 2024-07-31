using System.Collections.Generic;

namespace InGame.BattleFields.Enemies
{
    using Enemies = KeyValuePair<Enemy, int>;
    using EnemyGroups = KeyValuePair<EnemyGroup, int>;

    public class EnemyGroup
    {
        private List<Enemies> m_enemyGroup;
    }

    public class EnemyWave
    {
        private List<EnemyGroups> m_enemyWave;
    }

    public class EnemyBattle
    {
        private List<EnemyWave> m_enemyBattle;
    }
}