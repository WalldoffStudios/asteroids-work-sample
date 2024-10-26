using UnityEngine;

namespace Asteroids.Obstacles
{
    public interface IAsteroidFactory
    {
        void CreateAsteroid(Vector2 position, Vector2 direction, float speed);
    }
    public class AsteroidFactory : IAsteroidFactory
    {
        private readonly IAsteroidPool _asteroidPool;

        public AsteroidFactory(IAsteroidPool asteroidPool)
        {
            _asteroidPool = asteroidPool;
        }
        
        public void CreateAsteroid(Vector2 position, Vector2 direction, float speed)
        {
            Asteroid asteroid = _asteroidPool.GetAsteroid();
            asteroid.gameObject.SetActive(true);
            asteroid.Initialize(position, direction, speed);
        }
    }
}