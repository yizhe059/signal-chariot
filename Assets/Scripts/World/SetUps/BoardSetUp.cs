using System.Collections.Generic;
using UnityEngine;
using World.Modules;
using World.Views;

namespace World.SetUps
{
    [System.Serializable]
    public class ActiveModule
    {
        public BoardPosition pos;
        [Min(0)]
        public int moduleID;
        public string moduleName;
        public Module.Orientation orientation;
    }
    
    [System.Serializable]
    public class BoardSetUp
    {
        public int width, height;
        public float cellSize;
        public Vector3 originPosition;
        public List<BoardPosition> openSlots;
        public List<ActiveModule> modules;
        public SlotView slotPrefab;
        public Transform horizontalBorderPrefab, verticalBorderPrefab;
    }
}