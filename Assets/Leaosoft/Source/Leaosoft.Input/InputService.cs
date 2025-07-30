using Leaosoft.Services;
using UnityEngine;

namespace Leaosoft.Input
{
    /// <summary>
    /// The InputService provides the abstraction <see cref="IInputService"/> to expose all the players inputs.
    /// <seealso cref="ServiceLocator"/>
    /// </summary>

    [DisallowMultipleComponent]
    public sealed class InputService : GameService, IInputService
    {
        [Header("Input System")]
        private Inputs _inputs;
        private Inputs.LandMapActions _landActions;

        protected override void RegisterService()
        {
            ServiceLocator.RegisterService<IInputService>(this);
        }

        protected override void UnregisterService()
        {
            ServiceLocator.UnregisterService<IInputService>();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _inputs = new Inputs();

            _landActions = _inputs.LandMap;

            _inputs.Enable();
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            _inputs.Disable();
        }

        public Vector2 GetPlayerMovement()
        {
            return _landActions.Movement.ReadValue<Vector2>();
        }

        public Vector2 GetMouseDelta()
        {
            return _landActions.Look.ReadValue<Vector2>();
        }

        public bool GetPlayerJumpedThisFrame()
        {
            return _landActions.Jump.triggered;
        }
    }
}
