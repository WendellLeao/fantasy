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
            
            _eventService.AddEventListener<DamageableSpawnedEvent>(HandleDamageableSpawned);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _eventService.RemoveEventListener<DamageableSpawnedEvent>(HandleDamageableSpawned);
        }

        protected override void DisposeEntity(IEntity entity)
        {
            base.DisposeEntity(entity);

            if (entity is not HealthView healthView)
            {
                return;
            }
            
            healthView.OnDamageableDied -= HandleHealthViewDamageableDied;
        }

        private void HandleDamageableSpawned(DamageableSpawnedEvent damageableSpawnedEvent)
        {
            IDamageable damageable = damageableSpawnedEvent.Damageable;

            HealthView healthView = (HealthView)CreateEntity(healthViewPrefab, damageable.HealthBarParent);

            healthView.OnDamageableDied += HandleHealthViewDamageableDied;
            
            healthView.Initialize(_mainCamera, damageable);
            healthView.Begin();
        }

        private void HandleHealthViewDamageableDied(HealthView healthView)
        {
            DisposeEntity(healthView);
        }
    }
}
