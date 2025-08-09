using Fantasy.Events.Health;
using Fantasy.Domain.Health;
using Leaosoft;
using Leaosoft.Events;
using UnityEngine;

namespace Fantasy.UI.Health.Manager
{
    internal sealed class HealthViewManager : Leaosoft.Manager
    {
        [SerializeField]
        private GameObject healthViewPrefab;
        
        private Camera _mainCamera;
        private IEventService _eventService;

        public void Initialize(Camera mainCamera, IEventService eventService)
        {
            _mainCamera = mainCamera;
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

        protected override void DisposeEntity(IEntity entity)
        {
            base.DisposeEntity(entity);

            if (entity is not HealthView healthView)
            {
                return;
            }
            
            healthView.OnHealthDepleted -= HandleHealthDepleted;
        }

        private void HandleHealthSpawned(HealthSpawnedEvent healthSpawnedEvent)
        {
            IHealth health = healthSpawnedEvent.Health;

            HealthView healthView = (HealthView)CreateEntity(healthViewPrefab, health.HealthBarParent);

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
