using System;
using Asteroids.Borders;
using UnityEngine;
using VContainer;

namespace Asteroids
{
    public class PlayerFacade : MonoBehaviour
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
    }   
}
