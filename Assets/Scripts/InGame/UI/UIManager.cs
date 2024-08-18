using System.Collections.Generic;
using UnityEngine;

using Utils;
using Utils.Common;

namespace InGame.UI
{
    public enum UIElements
    {
        BattleProgress,
        BattleResult,
        AndroidStatus,
        BoardBar,
        NavigationBar,
        ModuleInfoCard,
    }

    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private List<string> m_uiPrefabPaths = new();
        private List<IHidable> m_uiElements = new();
        private Dictionary<UIElements, int> m_uiDisplayFlags = new();

        private void Awake()
        {
            SpawnUIList();
        }

        public void SpawnUIList()
        {
            foreach(string uiPath in m_uiPrefabPaths)
            {
                Resources.Load<GameObject>(Constants.GO_UI_COMMON_PATH + uiPath);
            }
        }


    }
}
