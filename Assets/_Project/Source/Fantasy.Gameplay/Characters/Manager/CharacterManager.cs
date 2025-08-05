using Fantasy.Events.Health;
using Fantasy.Domain.Health;
using Leaosoft.Events;
using UnityEngine;

namespace Fantasy.Gameplay.Characters.Manager
{
    internal sealed class CharacterManager : Leaosoft.Manager
    {
        [SerializeField]
        private GameObject[] characterPrefabs;
        [SerializeField]
        private Transform[] spawnPoints;
        
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IEventService _eventService;

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

            for (int i = 0; i < characterPrefabs.Length; i++)
            {
                GameObject characterPrefab = characterPrefabs[i];
                Character character = (Character)CreateEntity(characterPrefab, spawnPoints[i]);

                character.Initialize(_particleFactory, _weaponFactory);
                character.Begin();

                DispatchDamageableSpawnedEvent(character.Damageable);
            }
        }

        private void DispatchDamageableSpawnedEvent(IDamageable wizardDamageable)
        {
            _eventService.DispatchEvent(new DamageableSpawnedEvent(wizardDamageable));
        }
    }
}
