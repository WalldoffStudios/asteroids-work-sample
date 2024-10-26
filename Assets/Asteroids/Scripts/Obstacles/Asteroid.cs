using Asteroids.Borders;
using UnityEngine;
using VContainer;

namespace Asteroids.Obstacles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour, IWrapRecycler
    {
        private Rigidbody2D _rigidbody2D;
        private AsteroidPool _pool;

        private IRegisterRecyclableTransform _registerRecyclableTransform;
        private IUnregisterRecyclableTransform _unregisterRecyclableTransform;

        [Inject]
        public void Construct(
            IRegisterRecyclableTransform registerRecyclableTransform,
            IUnregisterRecyclableTransform unregisterRecyclableTransform)
        {
            _registerRecyclableTransform = registerRecyclableTransform;
            _unregisterRecyclableTransform = unregisterRecyclableTransform;
        }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void SetPool(AsteroidPool pool) => _pool = pool;

        public void Initialize(Vector2 position, Vector2 direction, float speed)
        {
            transform.position = position;
            _rigidbody2D.velocity = direction * speed;
            
            _registerRecyclableTransform.RegisterRecycleTransform(this);
        }


        public Transform RecycleTransform() => transform;

        public void ReturnTransformToPool()
        {
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            _unregisterRecyclableTransform.UnRegisterRecycleTransform(this);
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0.0f;
            _pool.ReleaseAsteroid(this);
        }
    }   
}
