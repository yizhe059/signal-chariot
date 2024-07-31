using UnityEngine;

using InGame.BattleFields.Common;

namespace InGame.BattleFields.Enemies
{
    public class Enemy
    {
        private LimitedProperty m_health;
        private UnlimitedProperty m_speed;
        private UnlimitedProperty m_damage;

        public LimitedProperty health { get { return m_health; }}
        public UnlimitedProperty speed { get { return m_speed;}}
        public UnlimitedProperty damage { get { return m_damage;}}
    }
}