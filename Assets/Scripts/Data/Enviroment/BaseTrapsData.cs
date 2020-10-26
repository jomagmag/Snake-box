using UnityEngine;

namespace Snake_box
{
    public class BaseTrapsData : ScriptableObject
    {
        public GameObject SpawnPos;
        public GameObject Prefab;
        public float Damage;
        public float ReloadTime;
        public TrapType Type;
    }
}
