using System;
using Asteroids.Bullets;
using Asteroids.Managers;
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
    
    public class PlayerShooting : ITickable, IUpdateWeapon, ILevelStateListener, IDisposable
    {
        private readonly Transform _transform;
        private readonly IBulletFactory _bulletFactory;
        private readonly ILevelStateSubscription _levelStateSubscription;
        private IWeapon _weapon;
        private LevelGameState _currentState;
        
        public PlayerShooting(
            Transform transform,
            IWeapon weapon,
            IBulletFactory bulletFactory,
            ILevelStateSubscription stateSubscription)
        {
            _transform = transform;
            _weapon = weapon;
            _bulletFactory = bulletFactory;
            _levelStateSubscription = stateSubscription;
            _levelStateSubscription.RegisterStateListener(this);
        }
        
        public void UpdateWeapon(IWeapon newWeapon)
        {
            _weapon = newWeapon;
        }
        
        public void OnLevelStateChanged(LevelGameState newState)
        {
            _currentState = newState;
        }
        
        public void Tick()
        {
            if(_currentState != LevelGameState.Playing) return;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector2 direction = _transform.up;
                float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;
                
                _bulletFactory.CreateBullet(_weapon.BulletType(), _transform.position + _transform.up * 0.8f, rotationAngle);
            }
        }

        public void Dispose()
        {
            _levelStateSubscription.UnregisterStateListener(this);
        }
    }
}