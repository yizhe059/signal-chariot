using UnityEngine;

using Utils.Common;
using InGame.BattleFields.Common;

namespace InGame.Views
{  
    public class ModView : MonoBehaviour, IPickable
    {
        private Mod m_mod;
        public void Init(Mod mod)
        {
            m_mod = mod;
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        public void PickUp()
        {
            m_mod.Pickup();
        }
    }
}