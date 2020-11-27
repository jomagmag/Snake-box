using System;
using System.Collections.Generic;
using UnityEngine;


namespace Snake_box
{
    [CreateAssetMenu(fileName = "WaveSettingsData", menuName = "Data/EnemySpawn/WaveSettingsData")]
    public class WaveSettingsData : ScriptableObject
    {
        public int BasicEnemyPoints;
        public int BonusEnemyPoints;
        
        public List<Wave> Waves;
    }


}
