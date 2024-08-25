using UnityEngine;

using Utils.Common;

namespace InGame.BattleEffects
{
    public class DestroyAndSpawnEffect : Effect
    {
        private readonly GameObject m_owner;
        private readonly GameObject[] m_objectsToSpawn;

        public DestroyAndSpawnEffect(GameObject owner, GameObject[] objectsToSpawn) : base(1)
        {
            this.m_owner = owner;
            this.m_objectsToSpawn = objectsToSpawn;
        }

        public override void Trigger()
        {
            if (!IsActive) return;
            
            // spawn
            foreach (var obj in m_objectsToSpawn)
                GameObject.Instantiate(obj, m_owner.transform.position, Quaternion.identity);
            
            // destroy
            var dieable = m_owner.GetComponent<IDieable>(); 
            dieable?.Die();

            m_count--;
        }
    }
}