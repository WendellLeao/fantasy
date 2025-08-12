using Fantasy.Events.Health;
using Leaosoft;
using Leaosoft.Events;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.UI.Health.Manager
{
    internal sealed class HealthViewManager : EntityManager<HealthView>
    {
        [SerializeField]
        private PoolData healthViewPoolData;
        
        private Camera _mainCamera;
        private IPoolingService _poolingService;
        private IEventService _eventService;

        public override void DisposeEntity(HealthView healthView)
        {
            base.DisposeEntity(healthView);
            
            healthView.OnHealthDepleted -= HandleHealthDepleted;
            
            _poolingService.ReleaseObjectToPool(healthView);
        }
        
        public void Initialize(Camera mainCamera, IPoolingService poolingService, IEventService eventService)
        {
            _mainCamera = mainCamera;
            _poolingService = poolingService;
            _eventService = eventService;
            
            base.Initialize();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            _eventService.AddEventListener<HealthSpawnedEvent>(HandleHealthSpawned);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _eventService.RemoveEventListener<HealthSpawnedEvent>(HandleHealthSpawned);
        }

        private void HandleHealthSpawned(HealthSpawnedEvent healthSpawnedEvent)
        {
            IHealth health = healthSpawnedEvent.Health;

            if (!_poolingService.TryGetObjectFromPool(healthViewPoolData.Id, health.HealthBarParent, out HealthView healthView))
            {
                return;
            }
            
            RegisterEntity(healthView);

            healthView.OnHealthDepleted += HandleHealthDepleted;
            
            healthView.Initialize(_mainCamera, health);
        }

        private void HandleHealthDepleted(HealthView healthView)
        {
            DisposeEntity(healthView);
        }
    }
}
