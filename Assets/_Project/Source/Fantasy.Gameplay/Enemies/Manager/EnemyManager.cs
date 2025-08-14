using Leaosoft;
using Leaosoft.Events;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Enemies.Manager
{
    internal sealed class EnemyManager : EntityManager<IEnemy>
    {
        [SerializeField]
        private EnemySpawner enemySpawner;
        
        private IPoolingService _poolingService;
        private IEventService _eventService;
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        
        public void Initialize(IPoolingService poolingService, IEventService eventService, IParticleFactory particleFactory,
            IWeaponFactory weaponFactory)
        {
            _poolingService = poolingService;
            _eventService = eventService;
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            
            base.Initialize();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            enemySpawner.OnEnemySpawned += HandleEnemySpawned;

            enemySpawner.SetUp(_poolingService, _eventService, _particleFactory, _weaponFactory);
        }
        
        protected override void OnDispose()
        {
            base.OnDispose();
            
            enemySpawner.OnEnemySpawned -= HandleEnemySpawned;

            enemySpawner.Dispose();
        }
        
        protected override void DisposeEntity(IEnemy enemy)
        {
            base.DisposeEntity(enemy);
            
            enemy.OnDied -= HandleEnemyDied;
        }
        
        private void HandleEnemySpawned(IEnemy enemy)
        {
            RegisterEntity(enemy);
            
            enemy.OnDied += HandleEnemyDied;
        }

        private void HandleEnemyDied(IEnemy enemy)
        {
            DisposeEntity(enemy);
            
            enemySpawner.RespawnEntity(enemy);
        }
    }
}
