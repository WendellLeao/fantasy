using UnityEngine;

namespace Fantasy.Gameplay
{
    internal interface IMoveableAgent
    {
        public Vector3 Velocity { get; }

        public void SetUp(ICameraProvider cameraProvider, IParticleFactory particleFactory);
        public void ResetPath();
    }
}
