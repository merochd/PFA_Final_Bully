using UnityEngine;

namespace Bully.Core
{
    public interface IMovable
    {
        void MoveTo(Vector2 destination, float speed, float stoppingDistance);
        void Stop();
    }
}