using Leaosoft;
using UnityEngine;
using UnityEngine.AI;

namespace Fantasy.Gameplay
{
    internal sealed class ClickToMove : EntityComponent
    {
        [SerializeField]
        private NavMeshAgent navMeshAgent;
        [SerializeField]
        private HumanoidAnimatorController humanoidAnimatorController;
        [SerializeField]
        private float lookRotationSpeed = 3f;
        
        private RaycastHit _cachedHitInfo;
        private ICameraProvider _cameraProvider;

        public void Initialize(ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            
            base.Initialize();
        }

        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);
            
            if (Input.GetMouseButtonDown(1))
            {
                HandleNavMeshAgentDestination();
            }
            
            humanoidAnimatorController.SetVelocity(navMeshAgent.velocity.magnitude);
        }

        private void HandleNavMeshAgentDestination()
        {
            Ray ray = _cameraProvider.MainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out _cachedHitInfo))
            {
                navMeshAgent.destination = _cachedHitInfo.point;
            }
        }
    }
}
