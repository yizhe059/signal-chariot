
using System;
using UnityEngine;

namespace Utils.Common
{
    [System.Serializable]
    public class Grid<T>
    {
        [SerializeField]
        protected int m_width, m_height;
        [SerializeField]
        protected float m_cellSize;
        [SerializeField]
        protected Vector3 m_originPosition;
        [SerializeField]
        protected T[,] m_gridArray;

        public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<T>, int, int, T> constructor)
        {
            m_width = width;
            m_height = height;
            m_cellSize = cellSize;
            m_originPosition = originPosition;
            m_gridArray = new T[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    m_gridArray[x, y] = constructor(this, x, y);
                }
            }
        }

        public int height => m_height;
        
        public int width => m_width;

        public float cellSize => m_cellSize;

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * m_cellSize + m_originPosition;
        }

        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition.x - m_originPosition.x) / m_cellSize);
            y = Mathf.FloorToInt((worldPosition.y - m_originPosition.y) / m_cellSize);
        }
        
        public void SetValue(int x, int y, T value)
        {
            if ((x >= 0 && x < m_width) && (y >= 0 && y < m_height))
            {
                m_gridArray[x, y] = value;
            }
            else
            {
                Debug.LogError($"Grid Out of Bound: {x}, {y}");
            }
            
        }

        public void SetValue(Vector3 worldPosition, T value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            
            SetValue(x, y, value);
        }

        public T GetValue(int x, int y)
        {
            if (m_gridArray == null)
            {
                Debug.LogError("Array is null");
                return default(T);
            }
            if (m_gridArray.Length == m_width) Debug.LogError($"Length does not match {m_gridArray.Length}");
            if ((x >= 0 && x < m_width) && (y >= 0 && y < m_height))
            {
                return m_gridArray[x, y];
            }
            else
            {
                Debug.Log($"Grid Out of Bound: {x}, {y}");
                return default(T);
            }
        }

        public T GetValue(Vector3 worldPosition)
        {
            GetXY(worldPosition, out var x, out var y);
            return GetValue(x, y);
        }
    }
    
}