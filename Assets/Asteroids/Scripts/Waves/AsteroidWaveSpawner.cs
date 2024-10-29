using System;
using Asteroids.Managers;
using VContainer.Unity;

namespace Asteroids.Waves
{
    public interface IUpdateWaveData
    {
        void UpdateWaveData(WaveData newWaveData);
    }

    public interface IRoundCompleted
    {
        void CompletedRound(bool lastRound);
    }
    
    public class AsteroidWaveSpawner : ITickable, ILevelStateListener, IUpdateWaveData, IDisposable
    {
        private readonly ILevelStateSubscription _stateSubscription;
        private LevelGameState _currentState;
        private WaveData _currentWaveData;
        
        public AsteroidWaveSpawner(ILevelStateSubscription stateSubscription)
        {
            _stateSubscription = stateSubscription;
            _stateSubscription.RegisterStateListener(this);
        }
        
        public void UpdateWaveData(WaveData newWaveData)
        {
            _currentWaveData = newWaveData;
        }
        
        public void Tick()
        {
            
        }

        public void OnLevelStateChanged(LevelGameState newState)
        {
            
        }

        public void Dispose()
        {
            
        }
    }   
}
