using UnityEngine;
using Bully.Core;

namespace Bully.Gameplay
{
    public class EnemyAttack : MonoBehaviour, IAttacker
    {
        private float _nextAttackTime;

        public bool IsOnCooldown => Time.time < _nextAttackTime;

        public void PerformAttack(IDamageable target, int damage)
        {
            if (target == null || IsOnCooldown) return;

            target.TakeDamage(damage);
            // Le cooldown sera mis à jour par une méthode de configuration 
            // ou via les data passées par le Brain.
        }

        public void ResetCooldown(float cooldownDuration)
        {
            _nextAttackTime = Time.time + cooldownDuration;
        }
    }
}