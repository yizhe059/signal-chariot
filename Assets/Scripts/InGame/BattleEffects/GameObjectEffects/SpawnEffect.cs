using UnityEngine;

using Utils.Common;

namespace InGame.BattleEffects
{
    public class SpawnEffect : Effect
    {
        private readonly GameObject[] m_objectsToSpawn;

        public SpawnEffect(GameObject[] objectsToSpawn) : base(1)
        {
            this.m_objectsToSpawn = objectsToSpawn;
        }

        public override void Trigger(GameObject go)
        {
            if (!IsActive) return;            
            foreach (var obj in m_objectsToSpawn)
                GameObject.Instantiate(obj, go.transform.position, Quaternion.identity);
            
            // TODO: move this to owner
            // var dieable = owner.GetComponent<IDieable>(); 
            // dieable?.Die();

            m_count--;
        }
    }
}