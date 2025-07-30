using Fantasy.Events.Health;
using Leaosoft.Events;
using UnityEngine;

namespace Fantasy.Gameplay.Goblins.Manager
{
    internal sealed class GoblinManager : Leaosoft.Manager
    {
        [SerializeField]
        private GameObject goblinPrefab;
        [SerializeField]
        private Transform spawnPoint;

        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IEventService _eventService;
        private Goblin _goblin;

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
            
            _goblin = (Goblin)CreateEntity(goblinPrefab, spawnPoint);

            _goblin.Initialize(_particleFactory, _weaponFactory);
            _goblin.Begin();
            
            DispatchDamageableSpawnedEvent();
        }

        private void DispatchDamageableSpawnedEvent()
        {
            _eventService.DispatchEvent(new DamageableSpawnedEvent(_goblin.Damageable));
        }
    }
}
