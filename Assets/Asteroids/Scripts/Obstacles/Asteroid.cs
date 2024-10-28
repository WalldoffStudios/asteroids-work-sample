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
    public class Asteroid : MonoBehaviour, IDamageable, IDisposable
    {
        private Rigidbody2D _rigidbody2D;
        private int _asteroidLevel;

        private IScreenBoundsRecycler _screenBoundsRecycler;
        private IScreenBoundsTransporter _screenBoundsTransporter;
        private IUpdateScore _updateScore;
        private IHandleAsteroidDestroyed _asteroidDestroyed;
        private IAsteroidPool _asteroidPool;

        [Inject]
        public void Construct(
            IUpdateScore updateScore,
            IScreenBoundsTransporter screenBoundsTransporter,
            IHandleAsteroidDestroyed asteroidDestroyed, IAsteroidPool asteroidPool
            )
        {
            _screenBoundsTransporter = screenBoundsTransporter;
            _updateScore = updateScore;
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

        //public void SetPool(AsteroidPool pool) => _pool = pool;

        public void Initialize(int asteroidLevel, Vector2 position, Vector2 direction, float speed)
        {
            _asteroidLevel = asteroidLevel; 
            transform.localScale = Vector3.one * _asteroidLevel;
            
            transform.position = position;
            _rigidbody2D.velocity = direction * speed;
            
            _screenBoundsTransporter.RegisterTransform(transform);
            //_screenBoundsRecycler.RegisterWrapRecycler(this);
        }
        
        public Transform RecycleTransform() => transform;

        public void ReturnTransformToPool() => ReturnToPool();

        private void ReturnToPool()
        {
            _screenBoundsTransporter.UnregisterTransform(transform);
            //_screenBoundsRecycler.UnregisterWrapRecycler(this);
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0.0f;
            _asteroidPool.ReleaseAsteroid(this);
            //_pool.ReleaseAsteroid(this);
        }

        public void TakeDamage(int damage)
        {
            _asteroidDestroyed.AsteroidDestroyed(transform.position, _asteroidLevel);
            _updateScore.UpdateScore(1);
            ReturnToPool();
        }

        public void Dispose()
        {
            ReturnToPool();
        }
    }   
}
