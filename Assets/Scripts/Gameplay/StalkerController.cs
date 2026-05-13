using UnityEngine;
using Bully.Core;
using System.Collections;

namespace Bully.Gameplay
{
    public class StalkerController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private EnemyDataSO data;
        [SerializeField] private float stoppingDistance = 1.2f;
        [SerializeField] private float retreatDuration = 3f;

        // Abstractions (SOLID)
        private ITargeter sensor;
        private IMovable mover;
        private IPunisher punisherModule; 
        private EnemyAttack attackModule; 
        
        private IDamageable playerDamageable;
        private PlayerController playerController;

        // État interne
        private bool _isDisengaged = false;

        private void Awake()
        {
            sensor = GetComponent<ITargeter>();
            mover = GetComponent<IMovable>();
            attackModule = GetComponent<EnemyAttack>();
            punisherModule = GetComponent<IPunisher>();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerDamageable = player.GetComponent<IDamageable>();
                playerController = player.GetComponent<PlayerController>();
            }
        }

        private void FixedUpdate()
        {
            if (_isDisengaged || sensor == null || !sensor.HasTarget) 
            {
                if (mover != null) mover.Stop();
                return;
            }

            mover.MoveTo(sensor.CurrentTarget, data.moveSpeed, stoppingDistance);
            HandleCombatSequence();
        }

        private void HandleCombatSequence()
        {
            if (playerDamageable == null) return;

            float distanceToPlayer = Vector2.Distance(transform.position, ((MonoBehaviour)playerDamageable).transform.position);
            float interactionRange = stoppingDistance + data.attackRange;

            // Si on est à portée de frappe
            if (distanceToPlayer <= interactionRange)
            {
                // 1. ATTAQUE SYSTÉMATIQUE (si hors cooldown)
                if (!attackModule.IsOnCooldown)
                {
                    ExecuteAttackCycle();

                    // 2. TENTATIVE DE VOL (seulement si Cowering)
                    if (playerController != null && playerController.isCowering)
                    {
                        ExecutePunishmentCycle();
                    }
                }
            }
        }

        private void ExecuteAttackCycle()
        {
            attackModule.PerformAttack(playerDamageable, data.attackDamage);
            attackModule.ResetCooldown(data.attackCooldown);
            Debug.Log("<color=orange>[STALKER] Frappe physique effectuée !</color>");
        }

        private void ExecutePunishmentCycle()
        {
            if (punisherModule == null) return;

            // Tentative de vol via l'interface
            if (punisherModule.ExecutePunishment(playerController.gameObject))
            {
                // Debug Jaune pour le vol réussi
                Debug.Log("<color=#FFFF00><b>[VOL] Objet dérobé au joueur pendant qu'il se cachait !</b></color>");
                
                // On déclenche la fuite seulement si le vol a réussi
                StartCoroutine(DisengageRoutine());
            }
        }

        private IEnumerator DisengageRoutine()
        {
            _isDisengaged = true;
            Debug.Log("<color=lightblue>[STALKER] Mission accomplie, je me retire.</color>");
            
            yield return new WaitForSeconds(retreatDuration);
            
            _isDisengaged = false;
        }
    }
}