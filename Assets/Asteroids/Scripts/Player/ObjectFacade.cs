using System;
using Asteroids.Borders;
using UnityEngine;
using VContainer;

namespace Asteroids
{
    public interface IGetObjectTransform
    {
        Transform GetTransform();
    }

    public interface IGetRigidBody
    {
        Rigidbody2D GetRigidBody();
    }
    public class ObjectFacade : MonoBehaviour, IGetObjectTransform, IGetRigidBody
    {
        [field: SerializeField] public Rigidbody2D RigidBody { get; private set; } = null;
        [field: SerializeField] public Collider2D Collider { get; private set; } = null;

        [Inject]
        private IRegisterWrappingTransform _registerWrappingTransform;

        [Inject]
        private IUnregisterWrappingTransform _unRegisterWrappingTransform;

        private void Start()
        {
            _registerWrappingTransform.RegisterTransform(transform);
        }

        private void OnDisable()
        {
            _unRegisterWrappingTransform.UnregisterTransform(transform);
        }

        public Transform GetTransform() => transform;
        public Rigidbody2D GetRigidBody()
        {
            return null;
        }
    }   
}
