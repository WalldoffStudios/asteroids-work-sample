using Asteroids.Borders;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Obstacles
{
    public class AsteroidSpawner : ITickable
    {
        private readonly IGetScreenBorderPosition _borderPositionProvider;
        private readonly IGetScreenMoveDirection _moveDirectionProvider;
        private readonly IAsteroidFactory _asteroidFactory;
        private readonly float _timePerSpawn;

        private float spawnTimer = 0.0f;
        
        public AsteroidSpawner(IGetScreenBorderPosition borderPositionProvider, IGetScreenMoveDirection moveDirectionProvider, IAsteroidFactory asteroidFactory, float timePerSpawn)
        {
            _borderPositionProvider = borderPositionProvider;
            _moveDirectionProvider = moveDirectionProvider;
            _asteroidFactory = asteroidFactory;
            _timePerSpawn = timePerSpawn;
        }


        public void Tick()
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= _timePerSpawn)
            {
                SpawnAsteroid();
                spawnTimer -= _timePerSpawn;
            }
        }

        private void SpawnAsteroid()
        {
            (Vector2 spawnPosition, BorderEdges edge) = _borderPositionProvider.BorderPositionWithEdge();
            Vector2 spawnDirection = _moveDirectionProvider.MoveDirection(edge);
            _asteroidFactory.CreateAsteroid(spawnPosition, spawnDirection, 10.0f);
        }
    }   
}
