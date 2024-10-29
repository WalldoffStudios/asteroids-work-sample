using UnityEngine;

namespace Asteroids.Borders
{
    public interface IWrapRecycler
    {
        Transform RecycleTransform();
        void ReturnTransformToPool();
    }
}