using System;
using UnityEngine;

namespace Asteroids.Waves
{
    [Serializable]
    public struct WaveObstacle
    {
        [Min(1)]public int level;
        [Min(0)]public float delayBeforeSpawn;
    }
    [CreateAssetMenu(fileName = "New wave data", menuName = "Asteroids/Waves/Wave data")]
    public class WaveData : ScriptableObject
    {
        [field: SerializeField] public WaveObstacle[] WaveObstacles { get; private set; } = null;
    }   
}
