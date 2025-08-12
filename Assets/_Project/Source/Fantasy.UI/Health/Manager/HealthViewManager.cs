using Fantasy.Events.Health;
using Leaosoft;
using Leaosoft.Events;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.UI.Health.Manager
{
    internal sealed class HealthViewManager : Leaosoft.Manager
    {
        [SerializeField]
        private PoolData healthViewPoolData;
        
        private Camera _mainCamera;
        private IPoolingService _poolingService;
        private IEventService _eventService;

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

        protected override void DisposeEntity(Entity entity)
        {
            base.DisposeEntity(entity);

            if (entity is not HealthView healthView)
            {
                return;
            }
            
            healthView.OnHealthDepleted -= HandleHealthDepleted;
            
            _poolingService.ReleaseObjectToPool(healthView);
        }

        private void HandleHealthSpawned(HealthSpawnedEvent healthSpawnedEvent)
        {
            IHealth health = healthSpawnedEvent.Health;

            if (!_poolingService.TryGetObjectFromPool(healthViewPoolData.Id, out HealthView healthView))
            {
                return;
            }
            
            AddEntity(healthView);

            healthView.gameObject.transform.SetParent(health.HealthBarParent, worldPositionStays: false);
            
            healthView.OnHealthDepleted += HandleHealthDepleted;
            
            healthView.Initialize(_mainCamera, health);
            healthView.Begin();
        }

        private void HandleHealthDepleted(HealthView healthView)
        {
            DisposeEntity(healthView);
        }
    }
}
