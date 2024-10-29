using UnityEngine;

namespace Asteroids.Bullets
{
    public interface IBulletFactory
    {
        void CreateBullet(BulletType bulletType, Vector2 position, float rotationAngle);
    }
}
