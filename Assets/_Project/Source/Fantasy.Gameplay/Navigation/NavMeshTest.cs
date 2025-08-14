using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Navigation
{
    internal sealed class NavMeshTest : EntityComponent, IMoveableAgent
    {
        public Vector3 Velocity => Vector3.zero;
        
        public void SetUp(ICameraProvider cameraProvider, IParticleFactory particleFactory)
        {
            base.SetUp();
        }

        public void ResetPath()
        { }
    }
}
