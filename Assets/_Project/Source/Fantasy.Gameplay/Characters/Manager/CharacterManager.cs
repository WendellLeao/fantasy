using Leaosoft;
using Leaosoft.Events;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Characters.Manager
{
    internal sealed class CharacterManager : EntityManager<ICharacter>
    {
        [SerializeField]
        private CharacterSpawner characterSpawner;
        
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

            characterSpawner.OnCharacterSpawned += HandleCharacterSpawned;
            
            characterSpawner.Initialize(_poolingService, _eventService, _particleFactory, _weaponFactory, _cameraProvider);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            characterSpawner.OnCharacterSpawned -= HandleCharacterSpawned;

            characterSpawner.Dispose();
        }

        protected override void DisposeEntity(ICharacter character)
        {
            base.DisposeEntity(character);
            
            character.OnDied -= HandleCharacterDied;
        }

        private void HandleCharacterSpawned(ICharacter character)
        {
            RegisterEntity(character);
            
            character.OnDied += HandleCharacterDied;
        }
        
        private void HandleCharacterDied(ICharacter character)
        {
            DisposeEntity(character);

            characterSpawner.RespawnEntity(character);
        }
    }
}
