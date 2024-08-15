using SetUps;
using UnityEditor;
using UnityEngine;

namespace Editors.Board
{
    public class ModuleRoot: MonoBehaviour
    {
        public SetUp setUp;
        [ContextMenu("Save Asset")]
        public void SaveAssets()
        {
            if (setUp == null)
            {
                Debug.Log("No setUp File");
                return;
            }
            setUp.moduleLibrary.Clear();
            var list = transform.Find("Modules").GetComponentsInChildren<ModuleEdit>();

            foreach (var moduleEdit in list)
            {
                setUp.moduleLibrary.Add(moduleEdit.CreateSetUp());
            }
            
            EditorUtility.SetDirty(setUp);
            Debug.Log("Save Asset");

        }
    }
}