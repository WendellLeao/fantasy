using System;
using Fantasy.Domain.Health;
using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Enemies
{
    public sealed class BasicEnemy : Entity, IMoveableAgent
    {
        public event Action<BasicEnemy> OnDied;
        
        [SerializeField]
        private GameObject smokeParticleObject;
        
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IHealth _health;

        public IHealth Health => _health;
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

            if (View is BasicEnemyView basicEnemyView)
            {
                // TODO: implement the enemy's moveable agent component
                basicEnemyView.Initialize(moveableAgent: this, _health, damageController, weaponHolder, _particleFactory);
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
            _particleFactory.EmitParticle(smokeParticleObject, transform.position, smokeParticleObject.transform.rotation);
            
            OnDied?.Invoke(this);
        }

        public void SetDestination(Vector3 position)
        { }
    }
}
