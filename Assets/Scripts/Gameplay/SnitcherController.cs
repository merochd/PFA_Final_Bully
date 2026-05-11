using UnityEngine;
using Bully.Core;

namespace Bully.Gameplay
{
    public class SnitcherController : MonoBehaviour
    {
        private ITargeter _sensor;
        private EnemyGrab _grabModule;

        private void Awake()
        {
            _sensor = GetComponent<ITargeter>();
            _grabModule = GetComponent<EnemyGrab>();
        }

        private void Update()
        {
            // 1. Logique de délation (Distance)
            if (_sensor != null && _sensor.IsDirectSight)
            {
                BullyEvents.TriggerPlayerSpotted(_sensor.CurrentTarget);
            }
        }

        // 2. Logique de Grab (Contact)
        private void OnTriggerEnter2D(Collider2D other)
        {
            // On vérifie si c'est le joueur
            if (other.CompareTag("Player"))
            {
                if (other.TryGetComponent<PlayerController>(out var player))
                {
                    _grabModule.TryGrab(player);
                }
            }
        }
    }
}