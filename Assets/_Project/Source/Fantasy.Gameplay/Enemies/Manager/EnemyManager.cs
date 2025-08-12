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

        public override void DisposeEntity(IEnemy enemy)
        {
            base.DisposeEntity(enemy);
            
            enemy.OnDied -= DisposeEntity;
            
            enemySpawner.ReleaseEnemy(enemy);
        }
        
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
            
            enemySpawner.Initialize(_poolingService, _eventService, _particleFactory, _weaponFactory);

            enemySpawner.OnEnemySpawned += HandleEnemySpawned;
            
            enemySpawner.SpawnEnemy();
        }
        
        protected override void OnDispose()
        {
            base.OnDispose();
            
            enemySpawner.OnEnemySpawned -= HandleEnemySpawned;

            enemySpawner.Dispose();
        }
        
        private void HandleEnemySpawned(IEnemy enemy)
        {
            RegisterEntity(enemy);
            
            enemy.OnDied += DisposeEntity;
        }
    }
}
