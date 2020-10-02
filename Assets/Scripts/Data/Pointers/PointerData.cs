using UnityEngine;


namespace Snake_box
{
    [CreateAssetMenu(fileName = "PointerData", menuName = "Data/Pointers/PointerData")]
    public class PointerData : ScriptableObject
    {
        public int Border;
        public GameObject Pointer;
        private Transform Target;
        private Vector2 PointerPosition;
    }
}
