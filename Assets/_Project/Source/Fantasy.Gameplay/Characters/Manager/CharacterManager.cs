using Fantasy.Events.Health;
using Leaosoft;
using Leaosoft.Events;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Characters.Manager
{
    internal sealed class CharacterManager : EntityManager<ICharacter>
    {
        [SerializeField]
        private PoolData characterPoolData;
        [SerializeField]
        private Transform spawnPoint;
        
        private IPoolingService _poolingService;
        private IEventService _eventService;
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private ICameraProvider _cameraProvider;

        public void Initialize(IPoolingService poolingService, IEventService eventService, IParticleFactory particleFactory,
            IWeaponFactory weaponFactory, ICameraProvider cameraProvider)
        {
            _poolingService = poolingService;
            _eventService = eventService;
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            _cameraProvider = cameraProvider;
            
            base.Initialize();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            if (!_poolingService.TryGetObjectFromPool(characterPoolData.Id, out Character character))
            {
                return;
            }
            
            RegisterEntity(character);
            
            character.transform.SetParent(spawnPoint, worldPositionStays: false);
            
            character.Initialize(_particleFactory, _weaponFactory, _cameraProvider);
            character.Begin();
            
            character.OnDied += HandleCharacterDied;
            
            DispatchHealthSpawnedEvent(character.Health);

            _cameraProvider.VirtualCamera.SetTarget(character.transform);
        }

        private void HandleCharacterDied(Character character)
        {
            character.OnDied -= HandleCharacterDied;
            
            character.Stop();
        }

        private void DispatchHealthSpawnedEvent(IHealth health)
        {
            _eventService.DispatchEvent(new HealthSpawnedEvent(health));
        }
    }
}
