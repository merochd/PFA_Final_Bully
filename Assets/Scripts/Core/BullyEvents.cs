using System;
using UnityEngine; // Nécessaire pour Vector3

namespace Bully.Core
{
    public static class BullyEvents
    {
        // --- ANCHORING ---
        public static Action<int, int> OnAnchoringChanged; 
        public static Action OnAnchoringDepleted;

        // --- GAME STATE ---
        public static Action<float> OnTimerUpdated; 
        public static Action OnRoundEnd;

        // --- DETECTION & AI ---
        // On passe la position (Vector3) du joueur pour que les ennemis sachent où aller
        public static Action<Vector3> OnPlayerSpotted;

        // --- TRIGGERS ---
        public static void TriggerAnchoringChanged(int current, int max) => OnAnchoringChanged?.Invoke(current, max);
        public static void TriggerAnchoringDepleted() => OnAnchoringDepleted?.Invoke();
        
        // Nouvelle méthode pour que le Snitcher puisse "crier" la position
        public static void TriggerPlayerSpotted(Vector3 position) => OnPlayerSpotted?.Invoke(position);
    }
}