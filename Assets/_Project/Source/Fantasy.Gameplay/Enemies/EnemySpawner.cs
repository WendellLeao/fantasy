using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fantasy.Events.Health;
using Leaosoft.Events;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Enemies
{
    internal sealed class EnemySpawner : MonoBehaviour
    {
        public event Action<IEnemy> OnEnemySpawned;
        
        [SerializeField]
        private PoolData enemyPoolData;
        [SerializeField]
        private Transform spawnPoint;
        [SerializeField]
        private float destroyEnemyObjectDelay = 7f;
        [SerializeField]
        private float respawnEnemyTimer = 2.5f;

        private IPoolingService _poolingService;
        private IEventService _eventService;
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private CancellationTokenSource _destroyEnemyObjectCts;

        public void Initialize(IPoolingService poolingService, IEventService eventService, IParticleFactory particleFactory,
            IWeaponFactory weaponFactory)
        {
            _poolingService = poolingService;
            _eventService = eventService;
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
        }
        
        public void Dispose()
        {
            _destroyEnemyObjectCts?.Cancel();
        }
        
        public void SpawnEnemy()
        {
            if (!_poolingService.TryGetObjectFromPool(enemyPoolData.Id, spawnPoint, out IEnemy enemy))
            {
                return;
            }
            
            enemy.Initialize(_particleFactory, _weaponFactory);

            DispatchHealthSpawnedEvent(enemy);

            OnEnemySpawned?.Invoke(enemy);
        }

        public void ReleaseEnemy(IEnemy enemy)
        {
            _destroyEnemyObjectCts?.Cancel();
            _destroyEnemyObjectCts = new CancellationTokenSource();
            
            ReleaseEnemyObjectAsync(enemy, _destroyEnemyObjectCts.Token).Forget();
        }
        
        private async UniTask ReleaseEnemyObjectAsync(IEnemy enemy, CancellationToken token)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(destroyEnemyObjectDelay), cancellationToken: token);

                GameObject basicEnemyGameObject = enemy.gameObject;
                
                if (!basicEnemyGameObject || token.IsCancellationRequested)
                {
                    return;
                }

                _poolingService.ReleaseObjectToPool(enemy);

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
        
        private void DispatchHealthSpawnedEvent(IEnemy enemy)
        {
            if (!enemy.gameObject.TryGetComponent(out IHealth health))
            {
                return;
            }
            
            _eventService.DispatchEvent(new HealthSpawnedEvent(health));
        }
    }
}
