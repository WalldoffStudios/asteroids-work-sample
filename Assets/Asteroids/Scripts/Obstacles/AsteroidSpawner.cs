using UnityEngine;

namespace Asteroids.Obstacles
{
    public class AsteroidSpawner
    {
        private readonly IAsteroidFactory _asteroidFactory;
        
        public AsteroidSpawner(IAsteroidFactory asteroidFactory)
        {
            _asteroidFactory = asteroidFactory;
        }

        public void SpawnAsteroid(int level, Vector2 position, Vector2 direction, float speed)
        {
            _asteroidFactory.CreateAsteroid(level, position, direction, speed);
        }
    }   
}
