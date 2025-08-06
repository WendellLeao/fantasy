using Leaosoft;
using UnityEngine;
using UnityEngine.AI;

namespace Fantasy.Gameplay
{
    internal sealed class NavMeshClickMover : EntityComponent, IMoveableAgent
    {
        [SerializeField]
        private NavMeshAgent navMeshAgent;
        [SerializeField]
        private float lookRotationSpeed = 3f;
        
        private RaycastHit _cachedHitInfo;
        private ICameraProvider _cameraProvider;
        
        public Vector3 Velocity => navMeshAgent.velocity;

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
        }

        private void HandleNavMeshAgentDestination()
        {
            Ray ray = _cameraProvider.MainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out _cachedHitInfo))
            {
                SetDestination(_cachedHitInfo.point);
            }
        }
        
        public void SetDestination(Vector3 position)
        {
            navMeshAgent.destination = position;
        }
    }
}
