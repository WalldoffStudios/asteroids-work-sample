using System;
using Asteroids.Borders;
using Asteroids.Managers;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Obstacles
{
    public class IntervalAsteroidSpawner : ITickable, ILevelStateListener, IDisposable
    {
        private readonly AsteroidSpawner _spawner;
        private readonly IGetScreenBorderPosition _borderPositionProvider;
        private readonly IGetScreenMoveDirection _moveDirectionProvider;
        private readonly float _timePerSpawn;
        private readonly ILevelStateSubscription _stateSubscription;
        private LevelGameState _currentState;

        private float spawnTimer = 0.0f;
        
        public IntervalAsteroidSpawner(AsteroidSpawner spawner, 
            IGetScreenBorderPosition borderPositionProvider, 
            IGetScreenMoveDirection moveDirectionProvider, 
            float timePerSpawn,
            ILevelStateSubscription stateSubscription)
        {
            _spawner = spawner;
            _borderPositionProvider = borderPositionProvider;
            _moveDirectionProvider = moveDirectionProvider;
            _timePerSpawn = timePerSpawn;
            
            _stateSubscription = stateSubscription;
            _stateSubscription.RegisterStateListener(this);
        }
        
        public void OnLevelStateChanged(LevelGameState newState)
        {
            _currentState = newState;
            spawnTimer = 0.0f;
        }
        
        public void Tick()
        {
            if(_currentState != LevelGameState.Playing) return;
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= _timePerSpawn)
            {
                SpawnNewAsteroid();
                spawnTimer -= _timePerSpawn;
            }
        }
        
        private void SpawnNewAsteroid()
        {
            (Vector2 spawnPosition, BorderEdges edge) = _borderPositionProvider.BorderPositionWithEdge();
            Vector2 spawnDirection = _moveDirectionProvider.MoveDirection(edge);
            _spawner.SpawnAsteroid(2, spawnPosition, spawnDirection, 10.0f);
        }

        public void Dispose()
        {
            _stateSubscription.UnregisterStateListener(this);
        }
    }
}