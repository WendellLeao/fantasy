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

            characterSpawner.Initialize(_poolingService, _eventService, _particleFactory, _weaponFactory, _cameraProvider);
            
            SpawnCharacter();
        }

        private void SpawnCharacter()
        {
            ICharacter character = characterSpawner.SpawnCharacter();
            
            RegisterEntity(character);
            
            character.OnDied += HandleCharacterDied;
        }

        private void HandleCharacterDied(ICharacter character)
        {
            character.OnDied -= HandleCharacterDied;
            
            character.Stop();
        }
    }
}
