using System;
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
    public class TransformWrapHandler : IRegisterWrappingTransform, IUnregisterWrappingTransform, IFixedTickable, IDisposable
    {
        private readonly ScreenBoundsHandler _boundsHandler;
        private readonly HashSet<Transform> _transforms;
        private readonly HashSet<Transform> _transformsToAdd;
        private readonly HashSet<Transform> _transformsToRemove;

        public TransformWrapHandler(ScreenBoundsHandler boundsHandler)
        {
            _boundsHandler = boundsHandler;
            _transforms = new HashSet<Transform>();
            _transformsToAdd = new HashSet<Transform>();
            _transformsToRemove = new HashSet<Transform>();
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

            _transforms.UnionWith(_transformsToAdd);
            _transformsToAdd.Clear();
            
            _transforms.ExceptWith(_transformsToRemove);
            _transformsToRemove.Clear();
        }

        public void Dispose()
        {
            _transforms.Clear();
            _transformsToAdd.Clear();
            _transformsToRemove.Clear();
        }
    }
}
