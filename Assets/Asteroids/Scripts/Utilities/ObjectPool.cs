using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Pooling
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private HashSet<T> _objectsInPool = new HashSet<T>();
        private readonly Stack<T> _poolStack = new Stack<T>();
        private readonly Func<T> _createFunc;
        private readonly Action<T> _onGet;
        private readonly Action<T> _onRelease;
        private readonly int _initialSize;

        public ObjectPool(Func<T> createFunc, Action<T> onGet = null, Action<T> onRelease = null, int initialSize = 10)
        {
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            _onGet = onGet;
            _onRelease = onRelease;
            _initialSize = initialSize;
        }

        protected virtual void Start()
        {
            for (int i = 0; i < _initialSize; i++)
            {
                var obj = _createFunc();
                obj.gameObject.SetActive(false);
                _poolStack.Push(obj);
                _objectsInPool.Add(obj);
            }
        }

        public T Get()
        {
            T obj;
            if (_poolStack.Count > 0)
            {
                obj = _poolStack.Pop();
                _objectsInPool.Remove(obj);
            }
            else
            {
                obj = _createFunc();
            }

            _onGet?.Invoke(obj);
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void Release(T obj)
        {
            if (_objectsInPool.Contains(obj)) return;
            
            _onRelease?.Invoke(obj);
            obj.gameObject.SetActive(false);
            _poolStack.Push(obj);
            _objectsInPool.Add(obj);
        }
    }
}

