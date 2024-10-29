using System;
using Asteroids.Managers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.Obstacles
{
    public class AsteroidSplitter : IStartable, IAsteroidDestructionListener, ILevelStateListener, IDisposable
    {
        private readonly AsteroidSpawner _spawner;
        private readonly IAsteroidDestructionSubscription _asteroidDestructionSubscription;
        private readonly ILevelStateSubscription _stateSubscription;
        private LevelGameState _currentState;
        
        [Inject]
        public AsteroidSplitter(
            AsteroidSpawner spawner, 
            IAsteroidDestructionSubscription asteroidDestructionSubscription,
            ILevelStateSubscription stateSubscription
            )
        {
            _spawner = spawner;
            _asteroidDestructionSubscription = asteroidDestructionSubscription;
            _asteroidDestructionSubscription.RegisterDestructionListener(this);
            _stateSubscription = stateSubscription;
            _stateSubscription.RegisterStateListener(this);
        }
        
        public void Start()
        {
            Debug.Log("Setup Asteroid splitter");
        }
        
        public void OnLevelStateChanged(LevelGameState newState)
        {
            _currentState = newState;
        }
        
        public void OnAsteroidDestroyed(Vector2 position, int level)
        {
            if(_currentState != LevelGameState.Playing) return;
            if (level > 1)
            {
                //Todo: magical numbers, define it somewhere
                int numberOfAsteroids = 6;
                float angleIncrement = 360.0f / numberOfAsteroids;
                for (int i = 0; i < numberOfAsteroids; i++)
                {
                    float angleRadians = Mathf.Deg2Rad * (angleIncrement * i);
                    Vector2 spawnDirection = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
                    Vector3 spawnPosition = position + (spawnDirection * 0.25f);
                    
                    _spawner.SpawnAsteroid(level -1, spawnPosition, spawnDirection, 3.0f);
                }
            }
        }

        public void Dispose()
        {
            _asteroidDestructionSubscription.UnregisterDestructionListener(this);
            _stateSubscription.UnregisterStateListener(this);
        }
    }
}