using UnityEngine;

namespace Snake_box
{
    [CreateAssetMenu(fileName = "BombBonusData", menuName = "Data/Bonus/BombBonusData")]
    [HelpURL("https://docs.google.com/document/d/1IOd2HdOEs9zy7EtXg2AOfgpQXKVbB1VF2gZdF82xuyE/edit#heading=h.tklf4dyovqp")]
    public class BombBonusData : BonusData
    {
        public GameObject particle;
        public float particleTimer;
        public float explosionRadius;
        public float explosionDamage;
    }
}
