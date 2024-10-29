using System;
using Asteroids.Borders;
using Asteroids.Score;
using UnityEngine;
using VContainer;

namespace Asteroids.Obstacles
{
    public interface IDamageable
    {
        void TakeDamage(int damage);
    }
    
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour, IDamageable, IWrapRecycler
    {
        private Rigidbody2D _rigidbody2D;
        private int _asteroidLevel;

        private IScreenBoundsRecycler _screenBoundsRecycler;
        private IAddToScore _addToScore;
        private IHandleAsteroidDestroyed _asteroidDestroyed;
        private IAsteroidPool _asteroidPool;
        private bool isPooled;

        [Inject]
        public void Construct(
            IAddToScore addToScore,
            IScreenBoundsRecycler screenBoundsRecycler,
            IHandleAsteroidDestroyed asteroidDestroyed,
            IAsteroidPool asteroidPool
            )
        {
            _screenBoundsRecycler = screenBoundsRecycler;
            _addToScore = addToScore;
            _asteroidDestroyed = asteroidDestroyed;
            _asteroidPool = asteroidPool;
        }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            if (_rigidbody2D == null)
            {
                throw new Exception("Asteroid failed getting it's rigidbody");
            }
        }

        public void Initialize(int asteroidLevel, Vector2 position, Vector2 direction, float speed)
        {
            _asteroidLevel = asteroidLevel; 
            transform.localScale = Vector3.one * _asteroidLevel;
            
            transform.position = position;
            _rigidbody2D.velocity = direction * speed;
            
            _screenBoundsRecycler.RegisterWrapRecycler(this);
            isPooled = false;
        }
        
        public Transform RecycleTransform() => transform;

        public void ReturnTransformToPool()
        {
            if (gameObject.activeSelf == false)
            {
                Debug.LogError("Tried to return a asteroid that was inactive");
            }
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0.0f;
            if (isPooled == false)
            {
                _screenBoundsRecycler.UnregisterWrapRecycler(this);
                _asteroidPool.ReleaseAsteroid(this);
            }
            isPooled = true;
        }

        public void TakeDamage(int damage)
        {
            _addToScore.AddToScore(1);
            _asteroidDestroyed.AsteroidDestroyed(transform.position, _asteroidLevel);
            ReturnToPool();
        }
    }   
}
