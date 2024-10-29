using UnityEngine;

namespace Asteroids.Bullets
{
    public class BulletFactory : IBulletFactory
    {
        private readonly IBulletPool _bulletPool;
        private readonly BulletDataCollection _bulletDataCollection;

        public BulletFactory(IBulletPool bulletPool, BulletDataCollection bulletDataCollection)
        {
            _bulletPool = bulletPool;
            _bulletDataCollection = bulletDataCollection;
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
    }
}
