using System;
using System.Runtime.CompilerServices;
using InGame.Views;
using SetUps;
using UnityEngine;

namespace Editors.Enemies
{
    public class EnemyEdit: MonoBehaviour
    {
        public new string name = "";
        
        [Min(1)]
        public float maxHealth;
        
        [Min(0f)]
        public float defense;
        
        [Min(1f)]
        public float attack;
        
        [Min(0.01f)]
        public float attackDuration;
        
        [Min(0)]
        public float attackRadius;

        [Min(0)]
        public float speed;

        [Min(1)]
        public int modQuantity;

        [Min(1)]
        public int modQuality;

        public EnemyView enemyPrefab;
        
        private string m_prevName = "";
        
        private void OnValidate()
        {
            if (name != m_prevName)
            {
                m_prevName = name;
                gameObject.name = name != "" ? name : "No name";
            }
        }

        public EnemySetUp CreateEnemySetUp()
        {
            return new EnemySetUp
            {
                name = name,
                attack = attack,
                attackDuration = attackDuration,
                attackRadius = attackRadius,
                defense = defense,
                maxHealth = maxHealth,
                speed = speed,
                modQuantity = modQuantity,
                modQuality = modQuality,
                enemyPrefab = enemyPrefab
            };
        }
    }
}