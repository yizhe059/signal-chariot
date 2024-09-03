using System.Collections.Generic;

using UnityEngine;

using SetUps;

using InGame.BattleEffects;

using Utils.Common;

#if UNITY_EDITOR
using Editors.BattleEffects;
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

                var collisionEffects = new List<Effect>();
                var destructionEffects = new List<Effect>();
                foreach (var effectEdit in effectEdits)
                {
                    if(effectEdit.triggerType == TriggerType.Collision)
                        collisionEffects.Add(effectEdit.CreateEffect());
                    else 
                        destructionEffects.Add(effectEdit.CreateEffect());
                }

                var bulletSetUp = edit.CreateBulletSetUp();
                bulletSetUp.collisionEffects = collisionEffects;
                bulletSetUp.destructionEffects = destructionEffects;
                setUp.bulletLibrary.Add(bulletSetUp);
            }

            Debug.Log("Save Asset");
        }
    }
}
#endif