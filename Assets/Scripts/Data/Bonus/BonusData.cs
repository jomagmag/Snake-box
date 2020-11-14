using UnityEngine;


namespace Snake_box
{
    [CreateAssetMenu(fileName = "BonusData", menuName = "Data/Bonus/BonusData")]
    public class BonusData : ScriptableObject
    {
        public GameObject prefab;
        public BonusType Type;
    }
}
