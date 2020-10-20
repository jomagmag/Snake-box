using UnityEngine;

namespace Snake_box
{
    [CreateAssetMenu(fileName = "RocketLauncherData", menuName = "Data/Level/Traps/RocketLauncherData")]
    public class RocketLauncherData : BaseTrapsData
    {
        public GameObject Projectile;
        public float ProjectileSpeed;
        
    }
}
