using UnityEngine;

namespace Asteroids.Bullets
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private float _speed;

        //Maybe add damage or something here
        public void SetData(float speed)
        {
            _speed = speed;
        }
        
        public void ShootBullet()
        {
            _rigidbody2D.AddForce(transform.up * (_speed + _rigidbody2D.velocity.magnitude), ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }
    }   
}
