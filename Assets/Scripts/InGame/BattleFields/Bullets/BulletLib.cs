using System.Collections.Generic;

using UnityEngine;

using SetUps;

namespace InGame.BattleFields.Bullets
{
    public enum BulletType
    {
        Basic,
        Laser,
        Landmine,
    }

    public class BulletLib
    {
        private Dictionary<BulletType, BulletSetUp> m_lib;
        public void Init(List<BulletSetUp> setUps)
        {
            m_lib = new();
            foreach (var setUp in setUps)
            {
                m_lib.Add(setUp.type, setUp);
            }
        }

        public BulletSetUp GetSetUp(BulletType type)
        {
            return m_lib[type];
        }
    }
}