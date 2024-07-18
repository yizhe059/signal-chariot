using UnityEngine;
namespace World
{
    [CreateAssetMenu(fileName = "Set Up", menuName = "Set Up", order = 0)]
    public class SetUp: ScriptableObject
    {
        public Board board;
    }
}