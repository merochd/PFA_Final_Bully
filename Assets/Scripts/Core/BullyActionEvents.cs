using System;

namespace Bully.Core
{
    public static class BullyEvents
    {
        // Exemple : Signal quand le joueur change de zone ou de santé
        public static Action<int> OnHealthChanged;
        public static void TriggerHealthChanged(int currentHealth) => OnHealthChanged?.Invoke(currentHealth);
    }
}