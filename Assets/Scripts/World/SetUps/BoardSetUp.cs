using System.Collections.Generic;
using UnityEngine;
using World.Views;

namespace World.SetUps
{
    [System.Serializable]
    public class BoardSetUp
    {
        public int width, height;
        public float cellSize;
        public Vector3 originPosition;
        public List<BoardPosition> openSlots;
        
        public SlotView slotPrefab;
        public Transform horizontalBorderPrefab, verticalBorderPrefab;
    }
}