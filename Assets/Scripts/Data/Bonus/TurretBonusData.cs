using UnityEngine;

namespace Snake_box
{
    [CreateAssetMenu(fileName = "TurretBonusData", menuName = "Data/Bonus/TurretBonusData")]
    [HelpURL("https://docs.google.com/document/d/1ppBT14FDl2LKykDnQ6Rvc-60SWl0Ys5d25SQJjv5sz4/edit")]
    public class TurretBonusData : BonusData
    {
        [Header("? для открытия документации")]
        public float PercentageA;
        public float PercentageB;
    }
}
