using System.Collections.Generic;

using UnityEngine;

using SetUps;

using InGame.BattleEffects;

using Editors.BattleEffects;

using Utils.Common;

#if UNITY_EDITOR
using UnityEditor;

namespace Editors.Bullets
{
    public class BulletRoot : MonoSingleton<BulletRoot>
    {
        public SetUp setUp;
        
        [ContextMenu("Save Asset")]
        public void SaveAssets()
        {
            setUp.bulletLibrary.Clear();
            var bulletEdits = gameObject.GetComponentsInChildren<BulletEdit>();
            
            foreach (var edit in bulletEdits)
            {        
                var effectEdits = edit.gameObject.GetComponents<EffectEdit>();
                var effects = new List<Effect>();
                foreach (var effectEdit in effectEdits)
                {
                    // select list based on trigger condition
                    // effects.Add(effectEdit.CreateEffect());
                }

                var bulletSetUp = edit.CreateBulletSetUp();
                // add effect list to setUp
                setUp.bulletLibrary.Add(bulletSetUp);
            }
        }
    }
}
#endif