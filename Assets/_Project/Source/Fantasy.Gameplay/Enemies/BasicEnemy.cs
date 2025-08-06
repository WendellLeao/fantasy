using Fantasy.Domain.Health;
using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Enemies
{
    public sealed class BasicEnemy : Entity, IMoveableAgent
    {
        [Header("View")]
        [SerializeField]
        private BasicEnemyView basicEnemyView;
        
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IDamageable _damageable;

        public IDamageable Damageable => _damageable;
        public Vector3 Velocity => Vector3.zero;

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
            
            // TODO: implement the enemy's moveable agent component
            basicEnemyView.Initialize(moveableAgent: this, _damageable, weaponHolder);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            basicEnemyView.Dispose();
        }

        public void SetDestination(Vector3 position)
        { }
    }
}
