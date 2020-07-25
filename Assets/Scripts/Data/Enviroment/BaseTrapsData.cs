using UnityEngine;

namespace Snake_box
{
    public class BaseTrapsData : ScriptableObject
    {
        public Transform SpawnPos;
        public GameObject Prefab;
        public float Damage;
        public float ReloadTime;
    }
}
