using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Bullets
{
    public enum BulletType
    {
        BlueBullet,
        RedBullet,
        GreenBullet,
        PurpleBullet
    }
    public interface IBulletFactory
    {
        Bullet CreateBullet(BulletType bulletType, Vector2 position, float rotationAngle, Transform parent);
    }
    
    public class BulletFactory : IBulletFactory
    {
        private readonly BulletDataCollection _bulletDataCollection;
        public BulletFactory(BulletDataCollection bulletDataCollection)
        {
            _bulletDataCollection = bulletDataCollection;
        }
        
        public Bullet CreateBullet(BulletType bulletType, Vector2 position, float rotationAngle, Transform parent)
        {
            BulletData bulletData = _bulletDataCollection.GetBulletDataByType(bulletType);
            //Bullet bullet = Object.Instantiate(bulletData.BulletPrefab, position, Quaternion.identity, parent);
            Bullet bullet = Object.Instantiate(bulletData.BulletPrefab, position, Quaternion.Euler(0.0f, 0.0f, rotationAngle), parent);
            bullet.SetData(bulletData.BulletSpeed);
            return bullet;
        }
    }   
}
