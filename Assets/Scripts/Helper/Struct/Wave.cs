using System.Collections.Generic;
using UnityEngine;

namespace Snake_box
{
    [System.Serializable]
    public struct Wave
    {
        [Min(1)]public int ActiveSpawnPoints;
        [Min(1)]public int WaveWeight;
        [Min(0)]public float WaveTiming;
        public List<SubWave> SuvWaves;
    }
}
