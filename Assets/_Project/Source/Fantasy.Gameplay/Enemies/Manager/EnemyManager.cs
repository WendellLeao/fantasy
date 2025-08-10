using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fantasy.Events.Health;
using Fantasy.Domain.Health;
using Leaosoft;
using Leaosoft.Events;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Enemies.Manager
{
    internal sealed class EnemyManager : Leaosoft.Manager
    {
        [SerializeField]
        private PoolData enemyPoolData;
        [SerializeField]
        private Transform spawnPoint;
        [SerializeField]
        private float destroyEnemyObjectDelay = 7f;
        [SerializeField]
        private float respawnEnemyTimer = 2.5f;

        private IPoolingService _poolingService;
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IEventService _eventService;
        private CancellationTokenSource _destroyEnemyObjectCts;

        public void Initialize(IPoolingService poolingService, IParticleFactory particleFactory, IWeaponFactory weaponFactory, IEventService eventService)
        {
            _poolingService = poolingService;
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            _eventService = eventService;
            
            base.Initialize();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            SpawnEnemy();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _destroyEnemyObjectCts?.Cancel();
        }

        protected override void DisposeEntity(IEntity entity)
        {
            base.DisposeEntity(entity);

            if (entity is not BasicEnemy basicEnemy)
            {
                return;
            }
            
            basicEnemy.OnDied -= HandleBasicEnemyDied;

            _destroyEnemyObjectCts?.Cancel();
            _destroyEnemyObjectCts = new CancellationTokenSource();
            
            ReleaseEnemyObjectAsync(basicEnemy, _destroyEnemyObjectCts.Token).Forget();
        }

        private async UniTask ReleaseEnemyObjectAsync(BasicEnemy basicEnemy, CancellationToken token)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(destroyEnemyObjectDelay), cancellationToken: token);

                GameObject basicEnemyGameObject = basicEnemy.gameObject;
                
                if (!basicEnemyGameObject || token.IsCancellationRequested)
                {
                    return;
                }

                _poolingService.ReleaseObjectToPool(basicEnemy);

                await UniTask.Delay(TimeSpan.FromSeconds(respawnEnemyTimer), cancellationToken: token);

                SpawnEnemy();
            }
            catch (OperationCanceledException)
            { }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            finally
            {
                _destroyEnemyObjectCts?.Dispose();
                _destroyEnemyObjectCts = null;
            }
        }
        
        private void SpawnEnemy()
        {
            if (!_poolingService.TryGetObjectFromPool(enemyPoolData.Id, out BasicEnemy basicEnemy))
            {
                return;
            }
            
            AddEntity(basicEnemy);
            
            basicEnemy.transform.SetParent(spawnPoint, worldPositionStays: false);
            
            basicEnemy.Initialize(_particleFactory, _weaponFactory);
            basicEnemy.Begin();
            
            basicEnemy.OnDied += HandleBasicEnemyDied;
            
            DispatchHealthSpawnedEvent(basicEnemy.Health);
        }

        private void HandleBasicEnemyDied(BasicEnemy basicEnemy)
        {
            DisposeEntity(basicEnemy);
        }
        
        private void DispatchHealthSpawnedEvent(IHealth health)
        {
            _eventService.DispatchEvent(new HealthSpawnedEvent(health));
        }
    }
}
