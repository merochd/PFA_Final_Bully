using UnityEngine;
using Bully.Core;

namespace Bully.Gameplay
{
    public class SnitcherController : MonoBehaviour
    {
        private ITargeter _sensor;

        private void Awake()
        {
            _sensor = GetComponent<ITargeter>();
        }

        private void Update()
        {
            // Si le sensor détecte le joueur (via la distance codée dans EnemySensor)
            if (_sensor != null && _sensor.IsDirectSight)
            {
                // On prévient tout le monde (les Stalkers)
                BullyEvents.TriggerPlayerSpotted(_sensor.CurrentTarget);
            }
        }
    }
}