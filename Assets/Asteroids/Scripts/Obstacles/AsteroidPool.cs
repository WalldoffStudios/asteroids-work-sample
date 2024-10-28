using System;
using System.Collections.Generic;
using Asteroids.Pooling;
using UnityEngine;
using VContainer;

namespace Asteroids.Obstacles
{
    public interface IAsteroidPool
    {
        Asteroid GetAsteroid();
        void ReleaseAsteroid(Asteroid asteroid);
    }
    public class AsteroidPool : IAsteroidPool
    {
        private readonly ObjectPool<Asteroid> _asteroidPool;
        private readonly Asteroid _asteroidPrefab;
        private readonly Transform _parentTransform;
        private readonly IObjectResolver _resolver;
        
        public AsteroidPool(Asteroid asteroidPrefab, Transform parentTransform, IObjectResolver resolver, int initialPoolSize = 10)
        {
            _asteroidPrefab = asteroidPrefab;
            _parentTransform = parentTransform;
            _resolver = resolver;
            if (_resolver == null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }
            
            _asteroidPool = new ObjectPool<Asteroid>(
                createFunc: () => CreateAsteroid(_asteroidPrefab),
                onGet: null,
                onRelease: null,
                initialSize: initialPoolSize
            );
        }

        private Asteroid CreateAsteroid(Asteroid prefab)
        {
            var asteroid = UnityEngine.Object.Instantiate(prefab, _parentTransform);
            asteroid.gameObject.SetActive(false);
            asteroid.SetPool(this);
            
            _resolver.Inject(asteroid);

            return asteroid;
        }

        public Asteroid GetAsteroid() => _asteroidPool.Get();

        public void ReleaseAsteroid(Asteroid asteroid) => _asteroidPool.Release(asteroid);
    }
}