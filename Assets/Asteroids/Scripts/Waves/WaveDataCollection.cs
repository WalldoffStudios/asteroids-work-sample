using UnityEngine;

namespace Asteroids.Waves
{
    [CreateAssetMenu(fileName = "Wave data collection", menuName = "Asteroids/Waves/Wave data collection")]
    public class WaveDataCollection : ScriptableObject
    {
        [field: SerializeField] public WaveData[] waveDataCollection { get; private set; } = null;
    }   
}
