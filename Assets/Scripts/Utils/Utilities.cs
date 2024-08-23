using UnityEngine;

namespace Utils
{
    public static class Utilities
    {
        public static Vector3 RandomPosition()
        {
            float x = (Random.Range(0, 2) == 1) ? Random.Range(-50, -9) : Random.Range(10, 50);
            float y = (Random.Range(0, 2) == 1) ? Random.Range(-50, -9) : Random.Range(10, 50);
            return new Vector3(x, y, 0);
        }
    }
}