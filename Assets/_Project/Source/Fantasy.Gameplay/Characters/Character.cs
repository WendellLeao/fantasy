using Fantasy.Domain.Health;
using Leaosoft;

namespace Fantasy.Gameplay.Characters
{
    public sealed class Character : Entity
    {
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IDamageable _damageable;
        private ICameraProvider _cameraProvider;

        public IDamageable Damageable => _damageable;

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

                _damageable = healthController;
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
                characterView.Initialize(navMeshClickMover, _damageable, weaponHolder, _particleFactory);
            }
        }
    }
}
