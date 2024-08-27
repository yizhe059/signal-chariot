using UnityEngine;

using InGame.BattleEffects;

#if UNITY_EDITOR

using UnityEditor;

namespace Editors.BattleEffects
{
    public class EffectEdit : MonoBehaviour
    {
        
    }

    [CustomEditor(typeof(EffectEdit))]
    public class EffectEditTemplate : Editor
    {
        public EffectType effectType = EffectType.None;

        public int count;

        [Header("Damage Effects")]
        public int damage;
        public float duration;
        public float interval;
        public float radius;
        public Vector3 center;

        [Header("Spawn & Destroy Effects")]
        public GameObject[] objectsToSpawn;

        public override void OnInspectorGUI()
        {
            // EffectListEditor effectContainer = (EffectListEditor)target;

            // 显示 EffectType 下拉列表
            effectType = (EffectType)EditorGUILayout.EnumPopup("Effect Type", effectType);
            EditorGUILayout.IntField("Trigger Condition", damage);

            // 根据选中的 EffectType 显示不同的参数
            switch (effectType)
            {
                case EffectType.SingleOnceDamageEffect:
                    damage = EditorGUILayout.IntField("Damage", damage);
                    break;

                case EffectType.SingleContinuousDamageEffect:
                    damage = EditorGUILayout.IntField("Damage", damage);
                    duration = EditorGUILayout.FloatField("Duration", duration);
                    interval = EditorGUILayout.FloatField("Interval", interval);
                    break;

                case EffectType.RangeOnceDamageEffect:
                    damage = EditorGUILayout.IntField("Damage", damage);
                    center = EditorGUILayout.Vector3Field("Center", center);
                    radius = EditorGUILayout.FloatField("Radius", radius);
                    break;

                case EffectType.RangeContinuousDamageEffect:
                    damage = EditorGUILayout.IntField("Damage", damage);
                    center = EditorGUILayout.Vector3Field("Center", center);
                    radius = EditorGUILayout.FloatField("Radius", radius);
                    duration = EditorGUILayout.FloatField("Duration", duration);
                    interval = EditorGUILayout.FloatField("Interval", interval);
                    break;

                case EffectType.SpawnAndDestroyEffect:
                    // SerializedProperty objectsToSpawnProperty = serializedObject.FindProperty("objectsToSpawn");
                    // EditorGUILayout.PropertyField(objectsToSpawnProperty, true);
                    break;
                
                case EffectType.BouncingEffect:
                    count = EditorGUILayout.IntField("Count", count);
                    break;
                
                case EffectType.PenetrationEffect:
                    count = EditorGUILayout.IntField("Count", count);
                    break;

                case EffectType.SplittingEffect:
                    count = EditorGUILayout.IntField("Count", count);
                    break;
            }

            if (GUILayout.Button("Create Effect"))
            {
                Effect newEffect = CreateEffectInstance();
                // effectContainer.AddEffect(newEffect);
            }
        }

        private Effect CreateEffectInstance()
        {
            switch (effectType)
            {
                case EffectType.SingleOnceDamageEffect:
                    return new SingleOnceDamageEffect(damage);

                case EffectType.SingleContinuousDamageEffect:
                    return new SingleContinuousDamageEffect(damage, duration, interval);

                case EffectType.RangeOnceDamageEffect:
                    return new RangeOnceDamageEffect(center, radius, damage);

                case EffectType.RangeContinuousDamageEffect:
                    return new RangeContinuousDamageEffect(center, radius, damage, duration, interval);

                case EffectType.SpawnAndDestroyEffect:
                    return new SpawnAndDestroyEffect(objectsToSpawn);
                
                case EffectType.BouncingEffect:
                    return new BouncingEffect(count);
                
                case EffectType.PenetrationEffect:
                    return new PenetrationEffect(count);
                
                case EffectType.SplittingEffect:
                    return new SplittingEffect(count);
                    
                default:
                    return null;
            }
        }
    }
}

#endif