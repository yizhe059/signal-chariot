using InGame.Effects;
using UnityEngine;

namespace Editors.Effects
{
    public abstract class EffectEdit: MonoBehaviour
    {
        public abstract Effect CreateEffect();
    }
}