using Fantasy.Events.Health;
using Leaosoft.Events;
using UnityEngine;

namespace Fantasy.Gameplay.Enemies.Manager
{
    internal sealed class EnemyManager : Leaosoft.Manager
    {
        [SerializeField]
        private GameObject enemyPrefab;
        [SerializeField]
        private Transform spawnPoint;

        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IEventService _eventService;

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
            
            BasicEnemy basicEnemy = (BasicEnemy)CreateEntity(enemyPrefab, spawnPoint);

            basicEnemy.Initialize(_particleFactory, _weaponFactory);
            basicEnemy.Begin();
            
            DispatchDamageableSpawnedEvent(basicEnemy.Damageable);
        }

        private void DispatchDamageableSpawnedEvent(IDamageable enemyDamageable)
        {
            _eventService.DispatchEvent(new DamageableSpawnedEvent(enemyDamageable));
        }
    }
}
