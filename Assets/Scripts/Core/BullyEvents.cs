using System;
using UnityEngine;

namespace Bully.Core
{
    public static class BullyEvents
    {
        // --- ANCHORING (Santé Mentale) ---
        public static Action<int, int> OnAnchoringChanged; 
        public static Action OnAnchoringDepleted;

        // --- GAME STATE ---
        public static Action<float> OnTimerUpdated; 
        public static Action OnRoundEnd;

        // --- DETECTION & AI ---
        public static Action<Vector3> OnPlayerSpotted;

        // --- EQUIPMENT & DEBUFFS ---
        // true = possédé/actif, false = perdu/cassé
        public static Action<bool> OnPhoneStateChanged;    // Gère la Minimap
        public static Action<bool> OnGlassesStateChanged;  // Gère la reconnaissance visuelle
        public static Action<bool> OnHeadsetStateChanged;  // Gère la régénération d'ancrage
        
        // Optionnel : Pour afficher un message UI lors de la perte
        public static Action<string> OnItemLostFeedback;

        // --- TRIGGERS ---
        
        // Anchoring
        public static void TriggerAnchoringChanged(int current, int max) => OnAnchoringChanged?.Invoke(current, max);
        public static void TriggerAnchoringDepleted() => OnAnchoringDepleted?.Invoke();
        
        // AI
        public static void TriggerPlayerSpotted(Vector3 position) => OnPlayerSpotted?.Invoke(position);

        // Equipment Triggers
        public static void TriggerPhoneStateChanged(bool state) => OnPhoneStateChanged?.Invoke(state);
        public static void TriggerGlassesStateChanged(bool state) => OnGlassesStateChanged?.Invoke(state);
        public static void TriggerHeadsetStateChanged(bool state) => OnHeadsetStateChanged?.Invoke(state);
        
        // Feedback Trigger
        public static void TriggerItemLostFeedback(string itemName) => OnItemLostFeedback?.Invoke(itemName);
    }
}