using UnityEngine;

namespace Bully.Core
{
    public interface ITargeter
    {
        Vector2 CurrentTarget { get; }
        bool HasTarget { get; }
        bool IsDirectSight { get; }
    }
}