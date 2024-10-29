using System;
using Asteroids.Managers;
using UnityEngine;

namespace Asteroids.Bullets
{
    public class BulletFactory : IBulletFactory, ILevelStateListener, IDisposable
    {
        private readonly IBulletPool _bulletPool;
        private readonly BulletDataCollection _bulletDataCollection;
        private readonly ILevelStateSubscription _stateSubscription;

        public BulletFactory(
            IBulletPool bulletPool,
            BulletDataCollection bulletDataCollection,
            ILevelStateSubscription stateSubscription)
        {
            _bulletPool = bulletPool;
            _bulletDataCollection = bulletDataCollection;
            _stateSubscription = stateSubscription;
            _stateSubscription.RegisterStateListener(this);
        }

        public void CreateBullet(BulletType bulletType, Vector2 position, float rotationAngle)
        {
            var bulletData = _bulletDataCollection.GetBulletDataByType(bulletType);
            if (bulletData == null)
            {
                Debug.LogError($"BulletData not found for BulletType {bulletType}");
                return;
            }

            var bullet = _bulletPool.GetBullet(bulletType);
            bullet.gameObject.SetActive(true);
            bullet.Initialize(bulletType, position, rotationAngle, bulletData.BulletSpeed);
        }

        public void OnLevelStateChanged(LevelGameState newState)
        {
            if(newState != LevelGameState.Playing && newState != LevelGameState.Initializing) _bulletPool.ClearPool();
        }

        public void Dispose()
        {
            _stateSubscription.UnregisterStateListener(this);
        }
    }
}
