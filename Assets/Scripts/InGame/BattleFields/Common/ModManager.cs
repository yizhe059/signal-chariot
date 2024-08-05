using UnityEngine;

namespace InGame.BattleFields.Common
{
    public class ModManager
    {
        private readonly Transform m_modsTransform;

        public ModManager()
        {
            m_modsTransform = new GameObject("Mods").transform;
        }
        
        public Mod CreateMod(int quality, Vector2 position)
        {
            var newMod = new Mod(quality, position);
            newMod.view.transform.parent = m_modsTransform;
            return newMod;
        }
    }
}