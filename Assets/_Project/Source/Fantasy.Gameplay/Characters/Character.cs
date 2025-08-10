using System;
using Fantasy.Domain.Health;
using Leaosoft;
using Leaosoft.Pooling;
using NaughtyAttributes;

namespace Fantasy.Gameplay.Characters
{
    public sealed class Character : Entity, IPooledObject
    {
        public event Action<Character> OnDied;
        
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IHealth _health;
        private ICameraProvider _cameraProvider;

        public IHealth Health => _health;
        public string PoolId { get; set; }
        
        public void Initialize(IParticleFactory particleFactory, IWeaponFactory weaponFactory, ICameraProvider cameraProvider)
        {
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            _cameraProvider = cameraProvider;
            
            base.Initialize();
        }

        protected override void InitializeComponents()
        {
            if (TryGetComponent(out HealthController healthController))
            {
                healthController.Initialize();

                _health = healthController;
            }
            
            if (TryGetComponent(out DamageController damageController))
            {
                damageController.Initialize(_health);
            }

            if (TryGetComponent(out WeaponHolder weaponHolder))
            {
                weaponHolder.Initialize(_weaponFactory);
            }

            if (TryGetComponent(out NavMeshClickMover navMeshClickMover))
            {
                navMeshClickMover.Initialize(_cameraProvider, _particleFactory);
            }

            if (View is CharacterView characterView)
            {
                characterView.Initialize(navMeshClickMover, _health, damageController, weaponHolder, _particleFactory);
            }
        }
        
        protected override void OnBegin()
        {
            base.OnBegin();

            _health.OnDepleted += HandleHealthDepleted;
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            _health.OnDepleted -= HandleHealthDepleted;
        }
        
        private void HandleHealthDepleted()
        {
            OnDied?.Invoke(this);
        }

#if UNITY_EDITOR
        [Button]
        public void BeginDebug()
        {
            Begin();
        }

        [Button]
        public void StopDebug()
        {
            Stop();
        }
#endif
    }
}
