namespace Asteroids.Bullets
{
    public interface IBulletPool
    {
        Bullet GetBullet(BulletType bulletType);
        void ReleaseBullet(BulletType bulletType, Bullet bullet);
    }
}