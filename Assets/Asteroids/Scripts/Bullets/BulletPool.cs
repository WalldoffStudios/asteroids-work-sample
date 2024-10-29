using System;
using System.Collections.Generic;
using Asteroids.Pooling;
using UnityEngine;
using VContainer;

namespace Asteroids.Bullets
{
    public class BulletPool : IBulletPool
    {
        private readonly HashSet<Bullet> _activeBullets = new HashSet<Bullet>();
        private readonly Dictionary<BulletType, ObjectPool<Bullet>> _bulletPools = new Dictionary<BulletType, ObjectPool<Bullet>>();
        private readonly BulletDataCollection _bulletDataCollection;
        private readonly Transform _parentTransform;
        private readonly IObjectResolver _resolver;
        private bool _isClearing;

        public BulletPool(BulletDataCollection bulletDataCollection, Transform parentTransform, IObjectResolver resolver, int initialPoolSize = 10)
        {
            _bulletDataCollection = bulletDataCollection != null ? bulletDataCollection : throw new ArgumentNullException(nameof(bulletDataCollection));
            _parentTransform = parentTransform;
            _resolver = resolver;
            if (_resolver == null)
            {
                throw new ArgumentNullException(nameof(resolver));
            }
            
            foreach (BulletType bulletType in _bulletDataCollection.AvailableBulletTypes)
            {
                var bulletData = _bulletDataCollection.GetBulletDataByType(bulletType);
                if (bulletData != null)
                {
                    var pool = new ObjectPool<Bullet>(
                        createFunc: () => CreateBullet(bulletData.BulletPrefab),
                        onGet: AddActiveBullet,
                        onRelease: RemoveActiveBullet,
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

        private Bullet CreateBullet(Bullet prefab)
        {
            var bullet = UnityEngine.Object.Instantiate(prefab, _parentTransform);
            bullet.gameObject.SetActive(false);
            
            _resolver.Inject(bullet);

            return bullet;
        }

        public Bullet GetBullet(BulletType bulletType)
        {
            if (_bulletPools.TryGetValue(bulletType, out var pool))
            {
                return pool.Get();
            }
            Debug.LogError($"No bullet pool found for BulletType {bulletType}. Ensure it's included in the BulletDataCollection.");
            return null;
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

        public void AddActiveBullet(Bullet bullet)
        {
            if(_isClearing == false) _activeBullets.Add(bullet);
        }

        public void RemoveActiveBullet(Bullet bullet)
        {
            if(_isClearing == false) _activeBullets.Remove(bullet);
        }

        public void ClearPool()
        {
            _isClearing = true;
            foreach (Bullet bullet in _activeBullets)
            {
                if(bullet.gameObject.activeSelf) ReleaseBullet(bullet.BulletType, bullet);
            }
            _activeBullets.Clear();
            _isClearing = false;
        }
    }
}
