using UnityEngine;

namespace Snake_box
{
    [CreateAssetMenu(fileName = "RapidFireBonusData", menuName = "Data/Bonus/RapidFireBonusData")]
    [HelpURL("https://docs.google.com/document/d/1IOd2HdOEs9zy7EtXg2AOfgpQXKVbB1VF2gZdF82xuyE/edit#heading=h.tklf4dyovqp")]
    public sealed class RapidFireBonusData : BonusData
    {
        public float RapidFireMultiplier;
        public float BonusDuration;
    }
}
