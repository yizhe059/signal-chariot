﻿using System.Collections.Generic;
using Editors.Effects;
using InGame.Boards.Modules;
using InGame.Boards.Modules.ModuleBuffs;
using InGame.Views;
using SetUps;
using UnityEngine;

namespace Editors.Board
{
    public class ModuleEdit: MonoBehaviour
    {
        public new string name = "";
        public string leftBox = "";
        public string rightBox = "";
        public string desc = "";
        public string tags = "";
        public string function = "";
        public Category category = Category.默认;
        public ModuleView prefab;
        public List<ModulePosition> otherPositions;
        public ModuleBuffType buffMask;
        
        private const float CellSize = 1f;

        private string m_prevName = "";
        
        private void OnValidate()
        {
            if (name != m_prevName)
            {
                m_prevName = name;
                gameObject.name = name != "" ? name : "No name";
            }
        }

        private void OnDrawGizmos()
        {
            // always draw the pivot
            Gizmos.color = Color.white;
            Gizmos.DrawCube(transform.position, new Vector3(1,1,1)* CellSize * 0.99f);
            
            Gizmos.color = Color.gray;
            foreach (var pos in otherPositions)
            {
                Gizmos.DrawCube(transform.position + CellSize * new Vector3(pos.x, pos.y), 
                    new Vector3(1,1,1)* CellSize * 0.99f);
            }
        }

        public ModuleSetUp CreateSetUp()
        {
            var signalEffectEdits = GetComponentInChildren<SignalEffectEdit>();
            var placingEffectEdits = GetComponentInChildren<PlacingEffectEdit>();
            var customEffectEdits = GetComponentInChildren<CustomEffectEdit>();
            bool hasCustomEffect = 
                customEffectEdits.CreateCustomEffect(out var req, out var customEffects);

            return new ModuleSetUp
            {
                name = name,
                category = category,
                leftBox = leftBox,
                rightBox = rightBox,
                desc = desc,
                tag = tags,
                function = function,
                otherPositions = new List<ModulePosition>(otherPositions),
                buffMask = buffMask,
                prefab = prefab,
                signalEffects = signalEffectEdits.CreateEffects(),
                signalMask = signalEffectEdits.mask,
                coolDown = signalEffectEdits.coolDown,
                consumptionMethod = signalEffectEdits.consumptionMethod,
                energyConsumption = signalEffectEdits.energyConsumption,
                maxStoredPerTrigger = signalEffectEdits.maxStoredPerTrigger,
                maxUses = signalEffectEdits.maxUses,
                placingEffects = placingEffectEdits.CreateEffects(),
                requirements = placingEffectEdits.CreateRequirements(),
                
                hasCustomEffect = hasCustomEffect,
                triggerRequirement = req,
                customEffects = customEffects
            };
        }
    }
}