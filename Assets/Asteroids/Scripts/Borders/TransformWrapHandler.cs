using System.Collections.Generic;
using VContainer.Unity;
using UnityEngine;

namespace Asteroids.Borders
{
    public interface IRegisterWrappingTransform
    {
        void RegisterTransform(Transform wrapperTransform);
    }
    public interface IUnregisterWrappingTransform
    {
        void UnregisterTransform(Transform wrapperTransform);
    }
    public class TransformWrapHandler : IRegisterWrappingTransform, IUnregisterWrappingTransform, IFixedTickable
    {
        private readonly ScreenWrapHandler _wrapHandler;
        private readonly HashSet<Transform> _transforms = new HashSet<Transform>();

        public TransformWrapHandler(ScreenWrapHandler wrapHandler)
        {
            _wrapHandler = wrapHandler;
        }

        public void RegisterTransform(Transform wrapperTransform)
        {
            _transforms.Add(wrapperTransform);
        }

        public void FixedTick()
        {
            foreach (var transform in _transforms)
            {
                if (transform == null || !transform.gameObject.activeInHierarchy)
                {
                    continue;
                }

                Vector2 newPosition = _wrapHandler.WrapPosition(transform.position);
                transform.position = newPosition;
            }
        }

        public void UnregisterTransform(Transform wrapperTransform)
        {
            _transforms.Remove(wrapperTransform);
        }
    }
}
