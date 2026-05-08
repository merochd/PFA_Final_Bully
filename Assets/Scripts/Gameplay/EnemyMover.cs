using UnityEngine;
using Bully.Core;

namespace Bully.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMover : MonoBehaviour, IMovable
    {
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void MoveTo(Vector2 destination, float speed, float stoppingDistance)
        {
            float distance = Vector2.Distance(rb.position, destination);

            if (distance > stoppingDistance)
            {
                Vector2 direction = (destination - rb.position).normalized;
                rb.linearVelocity = direction * speed;
            }
            else
            {
                Stop();
            }
        }

        public void Stop()
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}