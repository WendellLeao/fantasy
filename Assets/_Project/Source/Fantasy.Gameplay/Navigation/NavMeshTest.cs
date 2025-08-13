using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Navigation
{
    internal sealed class NavMeshTest : EntityComponent, IMoveableAgent
    {
        public Vector3 Velocity => Vector3.zero;
        
        public void Initialize(ICameraProvider cameraProvider, IParticleFactory particleFactory)
        {
            base.Initialize();
        }

        public void ResetPath()
        { }
    }
}
