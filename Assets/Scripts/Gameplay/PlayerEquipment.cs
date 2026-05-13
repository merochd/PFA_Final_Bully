using UnityEngine;
using Bully.Core;
using System.Collections.Generic;

namespace Bully.Gameplay
{
    public class PlayerEquipment : MonoBehaviour
    {
        [Header("Initial State")]
        [SerializeField] private bool startWithPhone = true;
        [SerializeField] private bool startWithGlasses = true;
        [SerializeField] private bool startWithHeadset = true;

        // États actuels (Privés pour l'encapsulation, modifiables via méthodes)
        [SerializeField] private bool _hasPhone;
        [SerializeField] private bool _hasGlasses;
        [SerializeField] private bool _hasHeadset;

        private void Start()
        {
            // Initialisation propre
            _hasPhone = startWithPhone;
            _hasGlasses = startWithGlasses;
            _hasHeadset = startWithHeadset;

            // On force la mise à jour au lancement
            RefreshAllEquipment();
        }

        public void LoseRandomItem()
        {
            // Liste des items encore possédés
            List<string> availableItems = new List<string>();
            if (_hasPhone) availableItems.Add("Phone");
            if (_hasGlasses) availableItems.Add("Glasses");
            if (_hasHeadset) availableItems.Add("Headset");

            if (availableItems.Count == 0) return;

            // Choix aléatoire
            string chosen = availableItems[Random.Range(0, availableItems.Count)];

            switch (chosen)
            {
                case "Phone":
                    _hasPhone = false;
                    BullyEvents.TriggerPhoneStateChanged(false);
                    BullyEvents.TriggerItemLostFeedback("Téléphone perdu !");
                    break;
                case "Glasses":
                    _hasGlasses = false;
                    BullyEvents.TriggerGlassesStateChanged(false);
                    BullyEvents.TriggerItemLostFeedback("Lunettes perdues !");
                    break;
                case "Headset":
                    _hasHeadset = false;
                    BullyEvents.TriggerHeadsetStateChanged(false);
                    BullyEvents.TriggerItemLostFeedback("Casque perdu !");
                    break;
            }
        }

        private void RefreshAllEquipment()
        {
            BullyEvents.TriggerPhoneStateChanged(_hasPhone);
            BullyEvents.TriggerGlassesStateChanged(_hasGlasses);
            BullyEvents.TriggerHeadsetStateChanged(_hasHeadset);
        }

        // Méthodes pour ramasser (utile pour plus tard)
        public void RecoverPhone() { _hasPhone = true; BullyEvents.TriggerPhoneStateChanged(true); }
        public void RecoverGlasses() { _hasGlasses = true; BullyEvents.TriggerGlassesStateChanged(true); }
        public void RecoverHeadset() { _hasHeadset = true; BullyEvents.TriggerHeadsetStateChanged(true); }
    }
}