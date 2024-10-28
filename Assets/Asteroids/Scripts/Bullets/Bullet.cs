using Asteroids.Borders;
using Asteroids.Obstacles;
using UnityEngine;
using VContainer;

namespace Asteroids.Bullets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Bullet : MonoBehaviour, IWrapRecycler
    {
        private Rigidbody2D _rigidbody;
        private IBulletPool _bulletPool;
        private BulletType _bulletType;
        private float _bulletSpeed;

        private IScreenBoundsRecycler _screenBoundsRecycler;

        [Inject]
        public void Construct(IScreenBoundsRecycler screenBoundsRecycler)
        {
            _screenBoundsRecycler = screenBoundsRecycler;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = 0;
        }

        public void SetPool(IBulletPool pool)
        {
            _bulletPool = pool;
        }

        public void Initialize(BulletType bulletType, Vector2 position, float rotationAngle, float bulletSpeed)
        {
            _bulletType = bulletType;
            _bulletSpeed = bulletSpeed;

            transform.position = position;
            transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
            
            Vector2 direction = transform.up;
            _rigidbody.velocity = direction * _bulletSpeed;
            
            _screenBoundsRecycler.RegisterWrapRecycler(this);
        }

        public Transform RecycleTransform()
        {
            return transform;
        }

        public void ReturnTransformToPool()
        {
            ReturnToPool();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(1);    
            }
            else
            {
                Debug.Log($"Bullet collided with object that can't be destroyed: {other.gameObject.name}");
            }
            
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            _screenBoundsRecycler.UnregisterWrapRecycler(this);

            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
            gameObject.SetActive(false);
            _bulletPool.ReleaseBullet(_bulletType, this);
        }
    }   
}
