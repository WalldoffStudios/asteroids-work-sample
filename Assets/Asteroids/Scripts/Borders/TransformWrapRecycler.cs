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
    
    public class TransformWrapRecycler : IRegisterRecyclableTransform, IUnregisterRecyclableTransform, IFixedTickable
    {
        private readonly ScreenBoundsHandler _boundsHandler;
        private readonly HashSet<IWrapRecycler> _recyclableTransforms = new HashSet<IWrapRecycler>();
        private readonly HashSet<IWrapRecycler> _recyclablesToAdd = new HashSet<IWrapRecycler>();
        private readonly HashSet<IWrapRecycler> _recyclableToRemove = new HashSet<IWrapRecycler>();

        public TransformWrapRecycler(ScreenBoundsHandler boundsHandler)
        {
            _boundsHandler = boundsHandler;
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

            foreach (IWrapRecycler wrapRecycler in _recyclablesToAdd)
            {
                _recyclableTransforms.Add(wrapRecycler);
                
            }
            _recyclablesToAdd.Clear();
            
            foreach (IWrapRecycler wrapRecycler in _recyclableToRemove)
            {
                _recyclableTransforms.Remove(wrapRecycler);
            }
            _recyclableToRemove.Clear();
        }
    }
}