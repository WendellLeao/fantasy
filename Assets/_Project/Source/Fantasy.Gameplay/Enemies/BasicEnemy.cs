using System;
using Fantasy.Domain.Health;
using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Enemies
{
    public sealed class BasicEnemy : Entity, IMoveableAgent
    {
        public event Action<BasicEnemy> OnDied;
        
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

            if (View is BasicEnemyView basicEnemyView)
            {
                // TODO: implement the enemy's moveable agent component
                basicEnemyView.Initialize(moveableAgent: this, _damageable, weaponHolder, _particleFactory);
            }
        }

        protected override void OnBegin()
        {
            base.OnBegin();

            _damageable.OnDied += HandleDamageableDied;
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            _damageable.OnDied -= HandleDamageableDied;
        }

        private void HandleDamageableDied()
        {
            OnDied?.Invoke(this);
        }

        public void SetDestination(Vector3 position)
        { }
    }
}
