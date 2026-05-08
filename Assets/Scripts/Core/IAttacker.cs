namespace Bully.Core
{
    public interface IAttacker
    {
        void PerformAttack(IDamageable target, int damage);
        bool IsOnCooldown { get; }
    }
}