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
        private readonly ScreenBoundsHandler _boundsHandler;
        private readonly HashSet<Transform> _transforms = new HashSet<Transform>();
        private readonly HashSet<Transform> _transformsToAdd = new HashSet<Transform>();
        private readonly HashSet<Transform> _transformsToRemove = new HashSet<Transform>();

        public TransformWrapHandler(ScreenBoundsHandler boundsHandler)
        {
            _boundsHandler = boundsHandler;
        }

        public void RegisterTransform(Transform wrapperTransform)
        {
            _transformsToAdd.Add(wrapperTransform);
        }
        
        public void UnregisterTransform(Transform wrapperTransform)
        {
            _transformsToRemove.Add(wrapperTransform);
        }

        public void FixedTick()
        {
            foreach (var transform in _transforms)
            {
                if (transform == null || !transform.gameObject.activeInHierarchy)
                {
                    continue;
                }

                Vector2 newPosition = _boundsHandler.WrapPosition(transform.position);
                transform.position = newPosition;
            }

            foreach (Transform transform in _transformsToAdd)
            {
                _transforms.Add(transform);
            }
            _transformsToAdd.Clear();
            
            foreach (Transform transform in _transformsToRemove)
            {
                _transforms.Remove(transform);
            }
            _transformsToRemove.Clear();
        }
    }
}
