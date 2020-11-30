using System;
using System.Collections.Generic;
using UnityEngine;

namespace Snake_box
{
    [CreateAssetMenu(fileName = "EnemySpawnSettingsData", menuName = "Data/EnemySpawn/EnemySpawnSettingsData")]
    public class EnemySpawnSettingsData : ScriptableObject
    {
        public List<EnemySettings> EnemySettings;
    }

    [Serializable]
    public struct EnemySettings
    {
        public EnemyType EnemyType;
        public int EnemySpawnCost;
        public int EnemyWeight;
        public int EnemyMinWave;
    }
    
}

