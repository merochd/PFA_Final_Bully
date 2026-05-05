using System;

namespace Bully.Core
{
    public static class BullyEvents
    {
        public static Action<int> OnHealthChanged;
        public static void TriggerHealthChanged(int currentHealth) => OnHealthChanged?.Invoke(currentHealth);
    }
}