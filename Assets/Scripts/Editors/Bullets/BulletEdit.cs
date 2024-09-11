using UnityEngine;
using InGame.BattleFields.Bullets;

using SetUps;

namespace Editors.Bullets
{
    public class BulletEdit : MonoBehaviour
    {
        [Header("Identifications")]
        public new string name = "";
        private string m_prevName = "";

        public BulletType type;
        
        [Min(1)]
        public int level;

        public Sprite sprite;

        [Header("Properties")]
        public MoveType moveType;

        [Min(0.1f)]
        public float size;
        
        [Min(-1)]
        public float health;
        
        [Min(0)]
        public float speed;
        
        [Min(0)]
        public float lifeTime;
        
        private void OnValidate()
        {
            if (name != m_prevName)
            {
                m_prevName = name;
                gameObject.name = name != "" ? name : "No name";
            }
        }

        public BulletSetUp CreateBulletSetUp()
        {
            return new BulletSetUp
            {
                name = name,
                type = type,
                level = level,
                sprite = sprite,
                size = size,
                health = health,
                speed = speed,
                lifeTime = lifeTime,
                moveType = moveType
            };
        }
    }
}