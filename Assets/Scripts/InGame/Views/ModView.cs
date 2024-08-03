using System;
using System.Collections.Generic;

using UnityEngine;

using InGame.BattleFields.Common;
using Utils;

namespace InGame.Views
{  
    public class ModView : MonoBehaviour {
        private Mod m_mod;
        public void Init(Mod mod)
        {
            m_mod = mod;
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Constants.CHARIOT_TAG))
                m_mod.Pickedup();
        }
    }
}