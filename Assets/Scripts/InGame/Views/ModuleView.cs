using System;
using InGame.Boards.Modules;
using UnityEngine;
using Utils.Common;

namespace InGame.Views
{
    public class ModuleView: MonoBehaviour
    {
        private Module m_module;
        private Animator m_animator;
        private GameObject m_range;
        public void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_range = transform.Find("Range")?.gameObject;
            HideRange();
        }
        

        public void PlayAnimation(string animationName)
        {
            m_animator.Play(animationName, -1, 0f);
        }

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

        public void DisplayRange()
        {
            m_range?.SetActive(true);
        }

        public void HideRange()
        {
            m_range?.SetActive(false);
        }
    }
}