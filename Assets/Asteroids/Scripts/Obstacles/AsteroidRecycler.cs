using UnityEngine;

namespace Asteroids.Obstacles
{
    public class AsteroidRecycler 
    {
        private readonly AsteroidSpawner _asteroidSpawner;
        public AsteroidRecycler(AsteroidSpawner spawner)
        {
            _asteroidSpawner = spawner;
        }
        // private readonly IAsteroidFactory _asteroidFactory;
        // public AsteroidRecycler(IAsteroidFactory asteroidFactory)
        // {
        //     _asteroidFactory = asteroidFactory;
        // }
        
        // public void AsteroidDestroyed(Vector2 position, int level)
        // {
        //     if (level > -1)
        //     {
        //         int numberOfAsteroids = 6;
        //         float angleIncrement = 360.0f / numberOfAsteroids;
        //         for (int i = 0; i < numberOfAsteroids; i++)
        //         {
        //             float angleRadians = Mathf.Deg2Rad * (angleIncrement * i);
        //             Vector2 spawnDirection = new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
        //             Vector3 spawnPosition = position + (spawnDirection * 0.25f);
        //         
        //             
        //             Debug.Log("Asteroid destroyed, perform some action here");
        //             
        //             //_asteroidFactory.CreateAsteroid(level -1, spawnPosition, spawnDirection, 10.0f);
        //         }
        //     }
        // }
    }
}