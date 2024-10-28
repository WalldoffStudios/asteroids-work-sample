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
    public class Asteroid : MonoBehaviour, IWrapRecycler, IDamageable, IDisposable
    {
        private Rigidbody2D _rigidbody2D;
        private AsteroidPool _pool;
        private int _asteroidLevel;

        private IScreenBoundsRecycler _screenBoundsRecycler;
        private IUpdateScore _updateScore;
        private IHandleAsteroidDestroyed _asteroidDestroyed;

        [Inject]
        public void Construct(
            IScreenBoundsRecycler screenBoundsRecycler,
            IUpdateScore updateScore,
            IHandleAsteroidDestroyed asteroidDestroyed
            )
        {
            _screenBoundsRecycler = screenBoundsRecycler;
            _updateScore = updateScore;
            _asteroidDestroyed = asteroidDestroyed;
        }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            if (_rigidbody2D == null)
            {
                throw new Exception("Asteroid failed getting it's rigidbody");
            }
        }

        public void SetPool(AsteroidPool pool) => _pool = pool;

        public void Initialize(int asteroidLevel, Vector2 position, Vector2 direction, float speed)
        {
            _asteroidLevel = asteroidLevel; 
            transform.localScale = Vector3.one * _asteroidLevel;
            
            transform.position = position;
            _rigidbody2D.velocity = direction * speed;
            
            _screenBoundsRecycler.RegisterWrapRecycler(this);
        }
        
        public Transform RecycleTransform() => transform;

        public void ReturnTransformToPool() => ReturnToPool();

        public void ReturnToPool()
        {
            _screenBoundsRecycler.UnregisterWrapRecycler(this);
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0.0f;
            _pool.ReleaseAsteroid(this);
        }

        public void TakeDamage(int damage)
        {
            _asteroidDestroyed.AsteroidDestroyed(transform.position, _asteroidLevel);
            _updateScore.UpdateScore(1);
            ReturnToPool();
        }

        public void Dispose()
        {
            Debug.Log("Returned asteroid to pool");
            ReturnToPool();
        }
    }   
}
