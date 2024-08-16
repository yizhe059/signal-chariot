using System;
using UnityEngine;

namespace Editors.ModuleBuffs
{
    public class ModuleBuffEdit: MonoBehaviour
    {
        public void OnValidate()
        {
            var id = transform.GetSiblingIndex();
            gameObject.name = $"Buff {id}";
        }
    }
}