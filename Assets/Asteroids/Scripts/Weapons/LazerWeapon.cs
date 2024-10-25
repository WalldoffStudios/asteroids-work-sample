using Asteroids.Bullets;
using UnityEngine;

namespace Asteroids.Weapons
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class LazerWeapon : MonoBehaviour, IWeapon
    {
        public BulletType BulletType()
        {
            return Bullets.BulletType.BlueBullet;
        }
    }   
}
