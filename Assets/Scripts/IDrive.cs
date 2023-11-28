using UnityEngine;

namespace Race
{
    public interface IDrive
    {
        Vector2 Move { get; }
        bool IsBraking { get; }
        void Enable();
    }
}