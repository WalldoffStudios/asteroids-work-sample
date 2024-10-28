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
    public class PlayerFacade : MonoBehaviour, IGetObjectTransform, IGetRigidBody
    {
        [field: SerializeField] public Rigidbody2D RigidBody { get; private set; } = null;
        [field: SerializeField] public Collider2D Collider { get; private set; } = null;

        [Inject]
        private IScreenBoundsTransporter _screenBoundsTransporter;

        private void Start()
        {
            _screenBoundsTransporter.RegisterTransform(transform);
        }

        private void OnDisable()
        {
            _screenBoundsTransporter.UnregisterTransform(transform);
        }

        public Transform GetTransform() => transform;
        public Rigidbody2D GetRigidBody()
        {
            return null;
        }
    }   
}
