using Asteroids.Bullets;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids
{
    public interface IWeapon
    {
        BulletType BulletType();
    }

    public interface UpdateWeapon
    {
        void UpdateWeapon(IWeapon newWeapon);
    }
    
    public class PlayerShooting : ITickable
    {
        private readonly Transform _transform;
        private readonly IBulletFactory _bulletFactory;
        public PlayerShooting(Transform transform, IBulletFactory bulletFactory)
        {
            _transform = transform;
            _bulletFactory = bulletFactory;
        }
        
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector2 direction = _transform.up;
                float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;
                
                Bullet bullet = _bulletFactory.CreateBullet(
                    BulletType.BlueBullet,
                    _transform.position + _transform.up * 0.8f,
                    rotationAngle,
                    null);
                
                bullet.ShootBullet();
            }
        }
    }
}