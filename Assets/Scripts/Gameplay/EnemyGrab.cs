using UnityEngine;
using System.Collections;

namespace Bully.Gameplay
{
    public class EnemyGrab : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float grabDuration = 2.0f;
        [SerializeField] private float grabCooldown = 3.0f;
        
        private bool _isGrabbing = false;
        private float _nextGrabTime = 0f;

        // Propriété pour que le Controller puisse vérifier l'état si besoin
        public bool CanGrab => !_isGrabbing && Time.time >= _nextGrabTime;

        public void TryGrab(PlayerController player)
        {
            if (!CanGrab || player.IsRestricted) return;
            
            StartCoroutine(GrabRoutine(player));
        }

        private IEnumerator GrabRoutine(PlayerController player)
        {
            _isGrabbing = true;
            player.IsRestricted = true;
            
            Debug.Log("<color=orange>[GRAB] Joueur immobilisé !</color>");

            yield return new WaitForSeconds(grabDuration);

            player.IsRestricted = false;
            _isGrabbing = false;
            
            // On fixe le moment où le prochain grab sera possible
            _nextGrabTime = Time.time + grabCooldown;
            
            Debug.Log("<color=green>[GRAB] Cooldown activé, joueur libre.</color>");
        }
    }
}