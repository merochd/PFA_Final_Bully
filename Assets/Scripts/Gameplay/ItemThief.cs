using UnityEngine;
using Bully.Core;

namespace Bully.Gameplay
{ 
    public class ItemThief : MonoBehaviour, IPunisher
    {
        [SerializeField] private float stealProbability = 60f;
        [SerializeField] private float cooldown = 10f;
        private float _lastAttemptTime = -100f;

        public bool ExecutePunishment(GameObject target)
        {
            // Vérification du cooldown (Responsabilité du module)
            if (Time.time < _lastAttemptTime + cooldown) return false;
            _lastAttemptTime = Time.time;
            
            // Vérification de la cible
            if (!target.TryGetComponent<PlayerEquipment>(out var equipment)) return false; 
            
            // Logique de probabilité
            if (Random.Range(0f, 100f) <= stealProbability)
            {
                equipment.LoseRandomItem();
                return true; // Succès du vol
            }
            return false; // Échec du jet de dés
        }
    }
}