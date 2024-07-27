using UnityEngine;
using World.Effects;

namespace Editors.Effects
{
    public abstract class EffectEdit: MonoBehaviour
    {
        public abstract Effect CreateEffect();
    }
}