using UnityEngine;

namespace Asteroids.Bullets
{
    [CreateAssetMenu(fileName = "New Bullet data", menuName = "Asteroids/Bullets/Bullet data")]
    public class BulletData : ScriptableObject
    {
        [field: SerializeField] public BulletType BulletType { get; private set; } = BulletType.BlueBullet;
        [field: SerializeField] public Bullet BulletPrefab { get; private set; } = null;
        [field: SerializeField] public float BulletSpeed { get; private set; } = 10.0f;
    }   
}
