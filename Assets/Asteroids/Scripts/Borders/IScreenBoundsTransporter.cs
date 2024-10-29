using UnityEngine;

namespace Asteroids.Borders
{
    public interface IScreenBoundsTransporter
    {
        void RegisterTransform(Transform wrapperTransform);
        void UnregisterTransform(Transform wrapperTransform);
    }
}