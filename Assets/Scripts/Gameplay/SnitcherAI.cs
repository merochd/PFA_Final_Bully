using UnityEngine;
using Bully.Core;

namespace Bully.Gameplay
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class SnitcherAI : MonoBehaviour
    {
        [SerializeField] private EnemyDataSO data;
        
        private CircleCollider2D viewCollider;
        private bool isPlayerVisible;
        private Transform playerTransform;

        private void Awake()
        {
            viewCollider = GetComponent<CircleCollider2D>();
            UpdateColliderRadius();
        }

        private void Update()
        {
            if (isPlayerVisible && playerTransform != null)
            {
                BullyEvents.TriggerPlayerSpotted(playerTransform.position);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerVisible = true;
                playerTransform = other.transform;
                Debug.Log("<color=yellow>Snitcher : Cible repérée !</color>");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerVisible = false;
                Debug.Log("<color=yellow>Snitcher : Cible perdue.</color>");
            }
        }

        // --- OUTILS ÉDITEUR ---

        private void UpdateColliderRadius()
        {
            if (viewCollider != null && data != null)
            {
                viewCollider.radius = data.detectionRange;
                viewCollider.isTrigger = true;
            }
        }

        // Met à jour le collider dès que tu changes une valeur dans le SO ou l'inspecteur
        private void OnValidate()
        {
            if (viewCollider == null) viewCollider = GetComponent<CircleCollider2D>();
            UpdateColliderRadius();
        }

        // Dessine le cercle jaune dans la vue Scène
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            float radius = (data != null) ? data.detectionRange : 1f;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}