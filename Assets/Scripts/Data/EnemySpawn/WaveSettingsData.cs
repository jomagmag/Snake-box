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

    [Serializable]
    public struct Wave
    {
        [Min(1)]public int ActiveSpawnPoints;
        [Min(1)]public int WaveWeight;
        [Min(0)]public float WaveTiming;
        public List<SubWave> SuvWaves;
    }
    
    [Serializable]
    public struct SubWave
    {
        [Min(0)]public int SubWaveWeight;
        [Min(0)]public int SubWaveTiming;
        [Min(1)]public int SubWaveActiveSpawnPulls;
    }
}
