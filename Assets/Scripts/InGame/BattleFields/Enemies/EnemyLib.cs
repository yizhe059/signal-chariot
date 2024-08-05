using System.Collections.Generic;
using SetUps;
using UnityEngine;

namespace InGame.BattleFields.Enemies
{
    public class EnemyLib
    {
        public List<Enemy> m_enemies = new();
        private readonly Transform m_enemyTransform;
        
        public EnemyLib(List<EnemySetUp> setUps)
        {
            for(int i = 0; i < setUps.Count; i++)
            {
                var setUp = setUps[i];
                m_enemies.Add(Enemy.CreateEnemy(setUp, i));
            }
            m_enemyTransform = new GameObject("Enemies").transform;
        }

        public Enemy CreateEnemy(int id)
        {
            if (id < 0 || id >= m_enemies.Count) return null;
            var enemy = Enemy.CreateEnemy(m_enemies[id]);
            enemy.GetView().transform.parent = m_enemyTransform;
            
            return enemy;
        }

        public void DestroyEnemy(Enemy enemy)
        {
            if (enemy == null)
            {
                Debug.LogError("Enemy is null");
                return;
            }
            enemy.SelfDestroy();
            
        }
    }
}