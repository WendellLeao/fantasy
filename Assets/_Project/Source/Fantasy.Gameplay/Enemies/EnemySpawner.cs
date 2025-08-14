using System;
using Fantasy.Events.Health;
using Leaosoft.Events;
using Leaosoft.Pooling;

namespace Fantasy.Gameplay.Enemies
{
    internal sealed class EnemySpawner : BasicEntitySpawner<IEnemy>
    {
        public event Action<IEnemy> OnEnemySpawned;

        private IEventService _eventService;
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;

        public void SetUp(IPoolingService poolingService, IEventService eventService, IParticleFactory particleFactory,
            IWeaponFactory weaponFactory)
        {
            _eventService = eventService;
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            
            base.SetUp(poolingService);
        }

        protected override IEnemy SpawnEntity()
        {
            IEnemy enemy = base.SpawnEntity();
            
            enemy.SetUp(_particleFactory, _weaponFactory);

            _eventService.DispatchEvent(new HealthSpawnedEvent(enemy.Health));

            OnEnemySpawned?.Invoke(enemy);

            return enemy;
        }
    }
}
