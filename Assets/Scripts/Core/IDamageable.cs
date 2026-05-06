namespace Bully.Core
{
    public interface IDamageable
    {
        int CurrentAnchoring { get; }
        void TakeDamage(int amount);
        void RestoreAnchoring(int amount);
    }
}