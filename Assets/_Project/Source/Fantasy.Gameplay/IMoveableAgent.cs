using UnityEngine;

namespace Fantasy.Gameplay
{
    public interface IMoveableAgent
    {
        public Vector3 Velocity { get; }

        public void Initialize(ICameraProvider cameraProvider, IParticleFactory particleFactory);
        public void ResetPath();
    }
}
