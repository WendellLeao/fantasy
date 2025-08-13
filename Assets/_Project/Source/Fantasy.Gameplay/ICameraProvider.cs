using UnityEngine;

namespace Fantasy.Gameplay
{
    public interface ICameraProvider
    {
        public Camera MainCamera { get; }
        public IVirtualCamera VirtualCamera { get; }
    }
}
