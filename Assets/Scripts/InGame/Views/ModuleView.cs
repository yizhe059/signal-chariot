﻿using InGame.Boards.Modules;
using UnityEngine;

namespace InGame.Views
{
    public class ModuleView: MonoBehaviour
    {
        private Module m_module;

        public void SetWorldPos(Vector3 pos)
        {
            transform.localPosition = pos;
        }

        public void SetGlobalWorldPos(Vector3 pos)
        {
            transform.position = pos;
        }

        public void Rotate()
        {
            m_module.Rotate();
            var rotation = m_module.GetRotationDegree();
            transform.rotation = Quaternion.Euler(0, 0, rotation);
            
        }
        
        public static ModuleView CreateModuleView(ModuleView prefab, Transform parent, Module module, Quaternion rotation)
        {
            var moduleView = Instantiate(prefab, parent);
            moduleView.m_module = module;
            moduleView.transform.rotation = rotation;
            return moduleView;
        }
    }
}