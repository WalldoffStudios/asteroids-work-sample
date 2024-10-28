using System;
using Asteroids.Borders;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Obstacles
{
    public interface IHandleAsteroidDestroyed
    {
        void AsteroidDestroyed(Vector2 position, int level);
    }
    public class IntervalAsteroidSpawner : ITickable, IStartable, IAsteroidDestructionListener, IDisposable
    {
        private readonly AsteroidSpawner _spawner;
        private readonly IGetScreenBorderPosition _borderPositionProvider;
        private readonly IGetScreenMoveDirection _moveDirectionProvider;
        private readonly float _timePerSpawn;
        private readonly IAsteroidDestructionSubscription _asteroidDestructionSubscription;

        private float spawnTimer = 0.0f;
        
        public IntervalAsteroidSpawner(AsteroidSpawner spawner, 
            IGetScreenBorderPosition borderPositionProvider, 
            IGetScreenMoveDirection moveDirectionProvider, 
            float timePerSpawn, 
            IAsteroidDestructionSubscription asteroidDestructionSubscription)
        {
            _spawner = spawner;
            _borderPositionProvider = borderPositionProvider;
            _moveDirectionProvider = moveDirectionProvider;
            _timePerSpawn = timePerSpawn;
            _asteroidDestructionSubscription = asteroidDestructionSubscription;
        }

        public void Start()
        {
            _asteroidDestructionSubscription.RegisterDestructionListener(this);
        }
        
        public void Tick()
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= _timePerSpawn)
            {
                (Vector2 spawnPosition, BorderEdges edge) = _borderPositionProvider.BorderPositionWithEdge();
                Vector2 spawnDirection = _moveDirectionProvider.MoveDirection(edge);
                _spawner.SpawnAsteroid(1, spawnPosition, spawnDirection, 10.0f);
                spawnTimer -= _timePerSpawn;
            }
        }

        public void OnAsteroidDestroyed(Vector2 position, int level)
        {
            if (level > 0)
            {
                int numberOfAsteroids = 6;
                float angleIncrement = 360.0f / numberOfAsteroids;
                for (int i = 0; i < numberOfAsteroids; i++)
                {
                    float angleRadians = Mathf.Deg2Rad * (angleIncrement * i);
                    Vector2 spawnDirection = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
                    Vector3 spawnPosition = position + (spawnDirection * 0.25f);
                    
                    _spawner.SpawnAsteroid(level -1, spawnPosition, spawnDirection, 10.0f);
                }
            }
        }

        public void Dispose()
        {
            _asteroidDestructionSubscription.UnregisterDestructionListener(this);
        }
    }
}