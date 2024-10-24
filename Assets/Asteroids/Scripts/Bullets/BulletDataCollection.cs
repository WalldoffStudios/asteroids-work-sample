using UnityEngine;

namespace Asteroids.Bullets
{
    [CreateAssetMenu(fileName = "New Bullet data collection", menuName = "Asteroids/Bullets/Bullet data collection")]
    public class BulletDataCollection : ScriptableObject
    {
        [SerializeField] private BulletData[] bulletDataCollection = null;
        
        public BulletData GetBulletDataByType(BulletType bulletType)
        {
            for (int i = 0; i < bulletDataCollection.Length; i++)
            {
                BulletData data = bulletDataCollection[i];
                if (data.BulletType == bulletType) return data;
            }

            Debug.LogError($"Didnt find BulletData for the type {bulletType}, you probably have forgotten to serialize it");
            return null;
        }
    }
}