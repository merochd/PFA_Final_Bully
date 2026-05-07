using UnityEngine;
using Bully.Core;

namespace Bully.Gameplay
{
    public class HealthHandler : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerStatsSO stats;
        
        private int _currentAnchoring;

        public int CurrentAnchoring => _currentAnchoring;
        private PlayerController playerController;

        private void Awake()
        {
            if (stats == null)
            {
                Debug.LogError($"HealthHandler sur {gameObject.name} n'a pas de PlayerStatsSO assigné !");
                return;
            }

            _currentAnchoring = stats.startingAnchoring;
            playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            // On initialise l'UI/Feedback au lancement
            BullyEvents.TriggerAnchoringChanged(_currentAnchoring, stats.maxAnchoring);
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0) return;

            int finalDamage = amount;

            // Si on est en train de se recroqueviller, on réduit les dégâts
            if (playerController != null && playerController.IsCowering)
            {
                // On réduit de X% (ex: 30% de réduction -> on multiplie par 0.7)
                float reduction = 1f - stats.coweringDamageReduction;
                finalDamage = Mathf.RoundToInt(amount * reduction);
        
                Debug.Log($"Dégâts réduits par le Cowering : {amount} -> {finalDamage}");
            }

            _currentAnchoring -= finalDamage;
            _currentAnchoring = Mathf.Clamp(_currentAnchoring, 0, stats.maxAnchoring);

            BullyEvents.TriggerAnchoringChanged(_currentAnchoring, stats.maxAnchoring);

            if (_currentAnchoring <= 0)
            {
                BullyEvents.TriggerAnchoringDepleted();
            }
            Debug.Log($"<color=cyan>Dégâts subis: {finalDamage} | Anchoring restant: {_currentAnchoring}</color>");
        }

        public void RestoreAnchoring(int amount)
        {
            if (amount <= 0) return;

            _currentAnchoring += amount;
            _currentAnchoring = Mathf.Min(_currentAnchoring, stats.maxAnchoring);

            BullyEvents.TriggerAnchoringChanged(_currentAnchoring, stats.maxAnchoring);
        }
    }
}