using UnityEngine;

namespace Snake_box
{
    [CreateAssetMenu(fileName = "ElectricityPoolData", menuName = "Data/Level/Traps/ElectricityPoolData")]
    public class ElectricityPoolData : BaseTrapsData
    {
        public float ActiveTime;
        public float DamageDelay;
    }
}
