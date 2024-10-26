using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Borders
{
    public interface IWrapRecycler
    {
        Transform RecycleTransform();
        void ReturnTransformToPool();
    }

    public interface IRegisterRecyclableTransform
    {
        void RegisterRecycleTransform(IWrapRecycler wrapRecycler);
    }

    public interface IUnregisterRecyclableTransform
    {
        void UnRegisterRecycleTransform(IWrapRecycler wrapRecycler);
    }
    
    public class TransformWrapRecycler : IRegisterRecyclableTransform, IUnregisterRecyclableTransform, IFixedTickable, IDisposable
    {
        private readonly ScreenBoundsHandler _boundsHandler;
        private readonly HashSet<IWrapRecycler> _recyclableTransforms;
        private readonly HashSet<IWrapRecycler> _recyclablesToAdd;
        private readonly HashSet<IWrapRecycler> _recyclableToRemove;

        public TransformWrapRecycler(ScreenBoundsHandler boundsHandler)
        {
            _boundsHandler = boundsHandler;
            _recyclableTransforms = new HashSet<IWrapRecycler>();
            _recyclablesToAdd = new HashSet<IWrapRecycler>();
            _recyclableToRemove = new HashSet<IWrapRecycler>();
        }

        public void RegisterRecycleTransform(IWrapRecycler wrapRecycler)
        {
            _recyclablesToAdd.Add(wrapRecycler);
        }

        public void UnRegisterRecycleTransform(IWrapRecycler wrapRecycler)
        {
            _recyclableToRemove.Add(wrapRecycler);
        }

        public void FixedTick()
        {
            foreach (IWrapRecycler wrapRecycler in _recyclableTransforms)
            {
                Transform transform = wrapRecycler.RecycleTransform();
                if (transform == null || !transform.gameObject.activeInHierarchy)
                {
                    continue;
                }

                if (_boundsHandler.IsOutsideScreen(transform.position))
                {
                    wrapRecycler.ReturnTransformToPool();
                }
            }
            
            _recyclableTransforms.UnionWith(_recyclablesToAdd);
            _recyclablesToAdd.Clear();
            
            _recyclableTransforms.ExceptWith(_recyclableToRemove);
            _recyclableToRemove.Clear();
        }
        
        public void Dispose()
        {
            _recyclableTransforms.Clear();
            _recyclablesToAdd.Clear();
            _recyclableToRemove.Clear();
        }
    }
}