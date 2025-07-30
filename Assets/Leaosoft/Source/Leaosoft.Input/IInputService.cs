using Leaosoft.Services;
using UnityEngine;

namespace Leaosoft.Input
{
    public interface IInputService : IGameService
    {
        public Vector2 GetPlayerMovement();
        public Vector2 GetMouseDelta();
        public bool GetPlayerJumpedThisFrame();
    }
}
