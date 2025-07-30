using Fantasy.Events.Health;
using Leaosoft.Events;
using UnityEngine;

namespace Fantasy.Gameplay.Wizards.Manager
{
    internal sealed class WizardManager : Leaosoft.Manager
    {
        [SerializeField]
        private GameObject wizardPrefab;
        [SerializeField]
        private Transform spawnPoint;

        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IEventService _eventService;
        private Wizard _wizard;

        public void Initialize(IParticleFactory particleFactory, IWeaponFactory weaponFactory, IEventService eventService)
        {
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            _eventService = eventService;
            
            base.Initialize();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            _wizard = (Wizard)CreateEntity(wizardPrefab, spawnPoint);
            
            _wizard.Initialize(_particleFactory, _weaponFactory);
            _wizard.Begin();
            
            DispatchDamageableSpawnedEvent();
        }

        private void DispatchDamageableSpawnedEvent()
        {
            _eventService.DispatchEvent(new DamageableSpawnedEvent(_wizard.Damageable));
        }
    }
}
