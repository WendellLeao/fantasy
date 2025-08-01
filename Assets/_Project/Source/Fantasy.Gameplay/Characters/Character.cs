using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Characters
{
    internal sealed class Character : Entity
    {
        [Header("View")]
        [SerializeField]
        private CharacterView characterView;
        
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IDamageable _damageable;

        public IDamageable Damageable => _damageable;

        public void Initialize(IParticleFactory particleFactory, IWeaponFactory weaponFactory)
        {
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            
            base.Initialize();
        }

        protected override void InitializeComponents()
        {
            if (TryGetComponent(out HealthController healthController))
            {
                healthController.Initialize();

                _damageable = healthController;
            }

            if (TryGetComponent(out WeaponHolder weaponHolder))
            {
                weaponHolder.Initialize(_weaponFactory);
            }

            if (TryGetComponent(out DamageableView damageableView))
            {
                damageableView.Initialize(_damageable, _particleFactory);
            }
            
            characterView.Initialize(weaponHolder);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            characterView.Dispose();
        }
    }
}
