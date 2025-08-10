using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;
using UnityEngine.AI;

namespace Fantasy.Gameplay
{
    internal sealed class NavMeshClickMover : EntityComponent, IMoveableAgent
    {
        [SerializeField]
        private NavMeshAgent navMeshAgent;
        [SerializeField]
        private LayerMask walkableLayerMask;
        
        [Header("Particle")]
        [SerializeField]
        private PoolData clickSurfaceParticlePoolData;
        
        private ICameraProvider _cameraProvider;
        private IParticleFactory _particleFactory;
        private RaycastHit _cachedHitInfo;

        public Vector3 Velocity => navMeshAgent.velocity;

        public void Initialize(ICameraProvider cameraProvider, IParticleFactory particleFactory)
        {
            _cameraProvider = cameraProvider;
            _particleFactory = particleFactory;
            
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

            if (Physics.Raycast(ray.origin, ray.direction, out _cachedHitInfo, maxDistance: Mathf.Infinity, walkableLayerMask))
            {
                Vector3 hitInfoPoint = _cachedHitInfo.point;
                
                SetDestination(hitInfoPoint);

                EmitClickOnSurfaceParticle(hitInfoPoint);
            }
        }

        private void EmitClickOnSurfaceParticle(Vector3 hitInfoPoint)
        {
            Vector3 particlePosition = hitInfoPoint + new Vector3(0f, 0.1f, 0f);

            GameObject clickSurfaceParticlePrefab = clickSurfaceParticlePoolData.Prefab;
            
            _particleFactory.EmitParticle(clickSurfaceParticlePoolData, particlePosition, clickSurfaceParticlePrefab.transform.rotation);
        }

        public void SetDestination(Vector3 position)
        {
            navMeshAgent.destination = position;
        }
    }
}
