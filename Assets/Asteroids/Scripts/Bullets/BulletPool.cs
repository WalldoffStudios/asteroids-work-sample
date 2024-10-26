using System;
using System.Collections.Generic;
using Asteroids.Pooling;
using UnityEngine;
using VContainer;

namespace Asteroids.Bullets
{
    public interface IBulletPool
    {
        Bullet GetBullet(BulletType bulletType);
        void ReleaseBullet(BulletType bulletType, Bullet bullet);
    }

    public class BulletPool : IBulletPool
    {
        private readonly Dictionary<BulletType, ObjectPool<Bullet>> _bulletPools = new Dictionary<BulletType, ObjectPool<Bullet>>();
        private readonly BulletDataCollection _bulletDataCollection;
        private readonly Transform _parentTransform;
        private readonly IObjectResolver _resolver;

        public BulletPool(BulletDataCollection bulletDataCollection, Transform parentTransform, IObjectResolver resolver, int initialPoolSize = 10)
        {
            _bulletDataCollection = bulletDataCollection ?? throw new ArgumentNullException(nameof(bulletDataCollection));
            _parentTransform = parentTransform;
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));

            // Initialize pools for each bullet type present in the BulletDataCollection
            foreach (BulletType bulletType in _bulletDataCollection.AvailableBulletTypes)
            {
                var bulletData = _bulletDataCollection.GetBulletDataByType(bulletType);
                if (bulletData != null)
                {
                    var pool = new ObjectPool<Bullet>(
                        createFunc: () => CreateBullet(bulletData.BulletPrefab, bulletType),
                        onGet: null,
                        onRelease: null,
                        initialSize: initialPoolSize
                    );
                    _bulletPools[bulletType] = pool;
                }
                else
                {
                    Debug.LogError($"BulletData for BulletType {bulletType} is null in BulletDataCollection.");
                }
            }
        }

        private Bullet CreateBullet(Bullet prefab, BulletType bulletType)
        {
            var bullet = UnityEngine.Object.Instantiate(prefab, _parentTransform);
            bullet.gameObject.SetActive(false);
            bullet.SetPool(this);
            //bullet.SetBulletType(bulletType);

            // Inject dependencies into the bullet
            _resolver.Inject(bullet);

            return bullet;
        }

        public Bullet GetBullet(BulletType bulletType)
        {
            if (_bulletPools.TryGetValue(bulletType, out var pool))
            {
                return pool.Get();
            }
            else
            {
                Debug.LogError($"No bullet pool found for BulletType {bulletType}. Ensure it's included in the BulletDataCollection.");
                return null;
            }
        }

        public void ReleaseBullet(BulletType bulletType, Bullet bullet)
        {
            if (_bulletPools.TryGetValue(bulletType, out var pool))
            {
                pool.Release(bullet);
            }
            else
            {
                Debug.LogError($"No bullet pool found for BulletType {bulletType}. Cannot release bullet.");
            }
        }
    }
}