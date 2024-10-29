using System;
using Asteroids.Managers;
using UnityEngine;

namespace Asteroids.Obstacles
{
    public interface IAsteroidFactory
    {
        void CreateAsteroid(int level, Vector2 position, Vector2 direction, float speed);
    }
    public class AsteroidFactory : IAsteroidFactory, ILevelStateListener, IDisposable
    {
        private readonly IAsteroidPool _asteroidPool;
        private readonly ILevelStateSubscription _stateSubscription;

        public AsteroidFactory(IAsteroidPool asteroidPool, ILevelStateSubscription stateSubscription)
        {
            _asteroidPool = asteroidPool;
            _stateSubscription = stateSubscription;
            _stateSubscription.RegisterStateListener(this);
        }
        
        public void CreateAsteroid(int level, Vector2 position, Vector2 direction, float speed)
        {
            Asteroid asteroid = _asteroidPool.GetAsteroid();
            asteroid.gameObject.SetActive(true);
            asteroid.Initialize(level, position, direction, speed);
        }

        public void OnLevelStateChanged(LevelGameState newState)
        {
            if(newState != LevelGameState.Playing && newState != LevelGameState.Initializing) _asteroidPool.ClearPool(); 
        }

        public void Dispose()
        {
            _stateSubscription.UnregisterStateListener(this);
        }
    }
}