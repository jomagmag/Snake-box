using UnityEngine;

namespace Snake_box
{
    [System.Serializable]
    public struct SubWave
    {
        [Min(0)] public int SubWaveWeight;
        [Min(0)] public int SubWaveTiming;
        [Min(1)] public int SubWaveActiveSpawnPoints;
    }
}
