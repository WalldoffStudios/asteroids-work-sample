using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Bullets
{
    [CreateAssetMenu(fileName = "New Bullet data collection", menuName = "Asteroids/Bullets/Bullet data collection")]
    public class BulletDataCollection : ScriptableObject
    {
        [SerializeField]
        private BulletData[] bulletDataCollection = null;
        private Dictionary<BulletType, BulletData> bulletDataDict;

        // New property to expose available bullet types
        public IEnumerable<BulletType> AvailableBulletTypes => bulletDataDict.Keys;

        private void OnEnable()
        {
            bulletDataDict = new Dictionary<BulletType, BulletData>();
            for (int i = 0; i < bulletDataCollection.Length; i++)
            {
                BulletData data = bulletDataCollection[i];
                if (data != null && !bulletDataDict.ContainsKey(data.BulletType))
                {
                    bulletDataDict[data.BulletType] = data;
                }
                else if (bulletDataDict.ContainsKey(data.BulletType))
                {
                    Debug.LogWarning($"Duplicate BulletData for BulletType {data.BulletType} detected in BulletDataCollection.");
                }
            }
        }

        public BulletData GetBulletDataByType(BulletType bulletType)
        {
            if (bulletDataDict.TryGetValue(bulletType, out var data))
            {
                return data;
            }
            else
            {
                Debug.LogError($"BulletData for BulletType {bulletType} not found in the current BulletDataCollection.");
                return null;
            }
        }
    }
}