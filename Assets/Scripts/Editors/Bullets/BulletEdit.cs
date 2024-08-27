using UnityEngine;
using InGame.BattleFields.Bullets;

using SetUps;

namespace Editors.Bullets
{
    public class BulletEdit : MonoBehaviour
    {
        public new string name = "";
        private string m_prevName = "";

        public Sprite sprite;
        
        [Min(0.1f)]
        public float size;
        
        public float health;
        
        [Min(0)]
        public float speed;
        
        [Min(0)]
        public float lifeTime;
        
        public MoveType moveType;

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