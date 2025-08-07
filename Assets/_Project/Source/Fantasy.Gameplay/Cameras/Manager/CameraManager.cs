using UnityEngine;

namespace Fantasy.Gameplay.Cameras.Manager
{
    public sealed class CameraManager : Leaosoft.Manager, ICameraProvider
    {
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private VirtualCamera virtualCamera;
        
        public IVirtualCamera VirtualCamera => virtualCamera;
        public Camera MainCamera => mainCamera;
    }
}
