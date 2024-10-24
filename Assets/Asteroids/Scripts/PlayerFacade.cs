using UnityEngine;

namespace Asteroids
{
    public class PlayerFacade : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D RigidBody { get; private set; } = null;
        [field: SerializeField] public Collider2D Collider { get; private set; } = null;
    }   
}
