using System;
using Fantasy.Events.Health;
using Leaosoft.Events;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Characters
{
    internal sealed class CharacterSpawner : MonoBehaviour
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
        }
        
        public ICharacter SpawnCharacter()
        {
            if (!_poolingService.TryGetObjectFromPool(characterPoolData.Id, spawnPoint, out ICharacter character))
            {
                return null;
            }

            character.Initialize(_particleFactory, _weaponFactory, _cameraProvider);
            
            DispatchHealthSpawnedEvent(character);
            
            return character;
        }

        private void DispatchHealthSpawnedEvent(ICharacter character)
        {
            if (!character.gameObject.TryGetComponent(out IHealth health))
            {
                return;
            }
            
            _eventService.DispatchEvent(new HealthSpawnedEvent(health));
        }
    }
}
