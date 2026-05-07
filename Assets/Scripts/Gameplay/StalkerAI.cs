using UnityEngine;
using Bully.Core;

namespace Bully.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class StalkerAI : MonoBehaviour
    {
        [SerializeField] private EnemyDataSO data;
        [SerializeField] private float stoppingDistance = 1.2f;
        
        private Rigidbody2D rb;
        private Transform playerTransform; // Pour le check d'attaque précis
        private Vector3 lastKnownPosition;
        private bool hasTarget = false;
        private float nextAttackTime;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            
            // On récupère quand même la référence du transform pour l'attaque, 
            // mais on ne l'utilise pas pour le mouvement.
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerTransform = player.transform;
        }

        private void OnEnable()
        {
            // S'abonne aux cris du Snitcher
            BullyEvents.OnPlayerSpotted += UpdateTargetPosition;
        }

        private void OnDisable()
        {
            // Se désabonne
            BullyEvents.OnPlayerSpotted -= UpdateTargetPosition;
        }

        private void UpdateTargetPosition(Vector3 pos)
        {
            lastKnownPosition = pos;
            hasTarget = true;
        }

        private void FixedUpdate()
        {
            // Si personne n'a balancé le joueur, on ne bouge pas
            if (!hasTarget) return;

            float distanceToLastPos = Vector2.Distance(transform.position, lastKnownPosition);

            // 1. MOUVEMENT vers la dernière position connue
            if (distanceToLastPos > stoppingDistance)
            {
                MoveTowardsLocation(lastKnownPosition);
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }

            // 2. ATTAQUE (uniquement si le vrai joueur est à portée)
            if (playerTransform != null)
            {
                float actualDistanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
                float maxAttackDistance = stoppingDistance + data.attackRange;

                if (actualDistanceToPlayer <= maxAttackDistance && Time.time >= nextAttackTime)
                {
                    AttackPlayer();
                }
            }
        }

        private void MoveTowardsLocation(Vector3 targetPos)
        {
            Vector2 direction = ((Vector2)targetPos - rb.position).normalized;
            rb.linearVelocity = direction * data.moveSpeed;
        }

        private void AttackPlayer()
        {
            if (playerTransform.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(data.attackDamage);
                nextAttackTime = Time.time + data.attackCooldown;
                // Debug.Log pour confirmer l'attaque
                Debug.Log("<color=red>Le Stalker frappe le joueur !</color>");
            }
        }
    }
}