using UnityEngine;
using Bully.Core;

namespace Bully.Gameplay
{
    public class StalkerController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private EnemyDataSO data;
        [SerializeField] private float stoppingDistance = 1.2f;

        // Références aux interfaces (Abstractions)
        private ITargeter sensor;
        private IMovable mover;
        private EnemyAttack attackModule; // On peut garder la classe concrète ici car c'est un module spécifique
        
        private IDamageable playerDamageable;

        private void Awake()
        {
            // On récupère les composants via leurs interfaces
            sensor = GetComponent<ITargeter>();
            mover = GetComponent<IMovable>();
            attackModule = GetComponent<EnemyAttack>();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerDamageable = player.GetComponent<IDamageable>();
            }
        }

        private void FixedUpdate()
        {
            if (sensor == null || !sensor.HasTarget) 
            {
                if (mover != null) mover.Stop();
                return;
            }

            // 1. Gérer le mouvement
            mover.MoveTo(sensor.CurrentTarget, data.moveSpeed, stoppingDistance);

            // 2. Gérer le combat
            if (playerDamageable != null)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, ((MonoBehaviour)playerDamageable).transform.position);
                
                // Portée d'attaque = zone d'arrêt + allonge du SO
                // Dans le FixedUpdate du StalkerController.cs
                if (distanceToPlayer <= (stoppingDistance + data.attackRange))
                {
                    if (!attackModule.IsOnCooldown)
                    {
                        attackModule.PerformAttack(playerDamageable, data.attackDamage);
                        attackModule.ResetCooldown(data.attackCooldown);
                        Debug.Log("<color=orange>Attaque exécutée !</color>");
                    }
                }
            }
        }
    }
}