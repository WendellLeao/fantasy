using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fantasy.Events.Health;
using Fantasy.Domain.Health;
using Leaosoft;
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
        [SerializeField]
        private float destroyEnemyObjectDelay = 7f;
        [SerializeField]
        private float respawnEnemyTimer = 2.5f;

        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IEventService _eventService;
        private CancellationTokenSource _destroyEnemyObjectCts;

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
            
            DestroyEnemyObjectAsync(basicEnemy.gameObject, _destroyEnemyObjectCts.Token).Forget();
        }

        private void SpawnEnemy()
        {
            BasicEnemy basicEnemy = (BasicEnemy)CreateEntity(enemyPrefab, spawnPoint);

            basicEnemy.Initialize(_particleFactory, _weaponFactory);
            basicEnemy.Begin();

            basicEnemy.OnDied += HandleBasicEnemyDied;
            
            DispatchDamageableSpawnedEvent(basicEnemy.Damageable);
        }

        private void HandleBasicEnemyDied(BasicEnemy basicEnemy)
        {
            DisposeEntity(basicEnemy);
        }
        
        private void DispatchDamageableSpawnedEvent(IDamageable enemyDamageable)
        {
            _eventService.DispatchEvent(new DamageableSpawnedEvent(enemyDamageable));
        }
        
        private async UniTask DestroyEnemyObjectAsync(GameObject basicEnemyGameObject, CancellationToken token)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(destroyEnemyObjectDelay), cancellationToken: token);

                if (!basicEnemyGameObject || token.IsCancellationRequested)
                {
                    return;
                }

                Destroy(basicEnemyGameObject);

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
    }
}
