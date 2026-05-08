using UnityEngine;
using Bully.Core;

namespace Bully.Gameplay
{
    public class EnemySensor : MonoBehaviour, ITargeter
    {
        [SerializeField] private EnemyDataSO data;
        
        public Vector2 CurrentTarget { get; private set; }
        public bool HasTarget { get; private set; }
        public bool IsDirectSight { get; private set; }

        private Transform playerTransform;

        private void Awake()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerTransform = player.transform;
        }

        private void OnEnable() => BullyEvents.OnPlayerSpotted += UpdateTargetFromExternal;
        private void OnDisable() => BullyEvents.OnPlayerSpotted -= UpdateTargetFromExternal;

        private void Update()
        {
            if (playerTransform == null) return;

            float distance = Vector2.Distance(transform.position, playerTransform.position);
            
            if (distance <= data.detectionRange)
            {
                // NOUVEAU : On vérifie si un obstacle est entre nous et le joueur
                if (CheckLineOfSight())
                {
                    IsDirectSight = true;
                    UpdateTarget(playerTransform.position);
                }
                else
                {
                    IsDirectSight = false;
                }
            }
            else
            {
                IsDirectSight = false;
            }
        }

        private bool CheckLineOfSight()
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            float distance = Vector2.Distance(transform.position, playerTransform.position);

            // On lance un rayon laser vers le joueur
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, data.obstacleLayer);

            // Si le rayon ne touche RIEN, c'est que la voie est libre
            // (Il ne touche pas le joueur car le joueur est sur un autre layer)
            return hit.collider == null;
        }

        private void UpdateTargetFromExternal(Vector3 position)
        {
            if (!IsDirectSight) UpdateTarget(position);
        }

        private void UpdateTarget(Vector2 position)
        {
            CurrentTarget = position;
            HasTarget = true;
        }

        public void ClearTarget() => HasTarget = false;

        // --- DEBUG VISUEL AMÉLIORÉ ---
        private void OnDrawGizmos()
        {
            if (data == null) return;
            
            Gizmos.color = IsDirectSight ? Color.red : Color.yellow;
            Gizmos.DrawWireSphere(transform.position, data.detectionRange);

            // Dessine une ligne vers le joueur si on est à portée
            if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= data.detectionRange)
            {
                Gizmos.color = IsDirectSight ? Color.green : Color.gray;
                Gizmos.DrawLine(transform.position, playerTransform.position);
            }
        }
    }
}