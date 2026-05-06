using System;
//using UnityEngine;

namespace Bully.Core
{
    public static class BullyEvents
    {
        // --- ANCHORING ---
        public static Action<int, int> OnAnchoringChanged; // (Valeur Actuelle, Valeur Max)
        public static Action OnAnchoringDepleted;

        // --- GAME STATE ---
        public static Action<float> OnTimerUpdated; // (Temps restant)
        public static Action OnRoundEnd;

        // --- TRIGGERS ---
        public static void TriggerAnchoringChanged(int current, int max) => OnAnchoringChanged?.Invoke(current, max);
        public static void TriggerAnchoringDepleted() => OnAnchoringDepleted?.Invoke();
    }
}