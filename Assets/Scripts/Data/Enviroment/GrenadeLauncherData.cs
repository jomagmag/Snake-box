using UnityEngine;

namespace Snake_box
{
    [CreateAssetMenu(fileName = "GrenadeLauncherData", menuName = "Data/Level/Traps/GrenadeLauncherData")]
    public class GrenadeLauncherData : BaseTrapsData
    {
        public GameObject Projectile;
        public float Radius;
    }
}
