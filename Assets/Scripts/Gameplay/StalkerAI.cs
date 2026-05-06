using UnityEngine;
using Bully.Core;

namespace Bully.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class StalkerAI : MonoBehaviour
    {
        [SerializeField] private EnemyDataSO data;
        
        private Rigidbody2D rb;
        private Transform playerTarget;
        private float nextAttackTime;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            // On cherche le joueur via le tag (assure-toi que ton Player a le tag "Player")
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerTarget = player.transform;
        }

        private void FixedUpdate()
        {
            if (playerTarget == null) return;

            MoveTowardsPlayer();
        }

        private void MoveTowardsPlayer()
        {
            Vector2 direction = (playerTarget.position - transform.position).normalized;
            rb.linearVelocity = direction * data.moveSpeed;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            // Vérifie si on touche quelque chose qui peut prendre des dégâts
            if (Time.time >= nextAttackTime)
            {
                if (collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.TakeDamage(data.attackDamage);
                    nextAttackTime = Time.time + data.attackCooldown;
                    Debug.Log("Le Stalker a frappé !");
                }
            }
        }
    }
}