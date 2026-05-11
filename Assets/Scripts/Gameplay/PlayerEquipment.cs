using UnityEngine;
using Bully.Core;

namespace Bully.Gameplay
{
    public class PlayerEquipment : MonoBehaviour
    {
        [Header("Equipment State")]
        public bool hasPhone = true;   // Minimap
        public bool hasGlasses = true; // Identification des ennemis
        public bool hasHeadphones = true; // Régénération d'ancrage

        [Header("UI & Effects")]
        [SerializeField] private GameObject minimapUI;
        
        public void DropRandomItem()
        {
            // Logique simple pour le proto : on perd un item à chaque grab
            if (hasPhone) { hasPhone = false; UpdateEquipment(); return; }
            if (hasGlasses) { hasGlasses = false; UpdateEquipment(); return; }
            if (hasHeadphones) { hasHeadphones = false; UpdateEquipment(); return; }
        }

        private void UpdateEquipment()
        {
            // 1. Gestion Téléphone
            minimapUI.SetActive(hasPhone);
            BullyEvents.TriggerPhoneStateChanged(hasPhone);

            // 2. Gestion Lunettes
            // On peut envoyer un événement pour dire aux icônes de changer
            //BullyEvents.TriggerGlassesStateChanged(hasGlasses);

            // 3. Gestion Casque
            // Le script de santé/ancrage lira cette variable
            //BullyEvents.TriggerHeadsetStateChanged(hasHeadphones);  

        }
    }
}