using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using World.Modules;
using World.SetUps;

namespace Editors.Board
{
    public class ModuleEdit: MonoBehaviour
    {
        public string name ="";
        public List<ModulePosition> otherPositions;
        
        private const float CellSize = 1f;

        private string m_prevName = "";
        private void OnValidate()
        {
            if (name != m_prevName)
            {
                m_prevName = name;
                if (name != "") gameObject.name = name;
                else gameObject.name = "No name";
            }
        }


        private void OnDrawGizmos()
        {
            // always draw the pivot
            Gizmos.color = Color.white;
            Gizmos.DrawCube(transform.position, new Vector3(1,1,1)* CellSize * 0.99f);
            
            Gizmos.color = Color.gray;
            foreach (var pos in otherPositions)
            {
                Gizmos.DrawCube(transform.position + CellSize * new Vector3(pos.x, pos.y), 
                    new Vector3(1,1,1)* CellSize * 0.99f);
            }
        }

        public ModuleSetUp CreateSetUp()
        {
            return new ModuleSetUp
            {
                name = name,
                otherPositions = new List<ModulePosition>(otherPositions)
            };
        }
    }
}