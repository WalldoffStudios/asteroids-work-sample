using Asteroids.Bullets;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids
{
    public interface IWeapon
    {
        BulletType BulletType();
    }

    public interface IUpdateWeapon
    {
        void UpdateWeapon(IWeapon newWeapon);
    }
    
    public class PlayerShooting : ITickable, IUpdateWeapon 
    {
        private readonly Transform _transform;
        private IWeapon _weapon;
        private readonly IBulletFactory _bulletFactory;
        public PlayerShooting(Transform transform, IWeapon weapon, IBulletFactory bulletFactory)
        {
            _transform = transform;
            _weapon = weapon;
            _bulletFactory = bulletFactory;
        }
        
        public void UpdateWeapon(IWeapon newWeapon)
        {
            _weapon = newWeapon;
        }
        
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector2 direction = _transform.up;
                float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;
                
                _bulletFactory.CreateBullet(_weapon.BulletType(), _transform.position + _transform.up * 0.8f, rotationAngle);
            }
        }
    }
}