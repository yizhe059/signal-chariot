using UnityEngine;

namespace Utils
{
    public static class Utilities
    {
        public static Vector3 RandomPosition()
        {
            float x = Random.Range(50, 100);
            float y = Random.Range(50, 100);
            return new Vector3(x, y, 0);
        }
    }
}