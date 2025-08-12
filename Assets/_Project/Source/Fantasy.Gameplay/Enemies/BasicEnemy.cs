using System;
using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Enemies
{
    public sealed class BasicEnemy : Entity, IEnemy, IPooledObject, IMoveableAgent
    {
        public event Action<BasicEnemy> OnDied;
        
        [SerializeField]
        private PoolData smokeParticlePoolData;
        
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IHealth _health;

        public IHealth Health => _health;
        public Vector3 Velocity => Vector3.zero;
        public string PoolId { get; set; }

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

            if (TryGetComponent(out CommandInvoker commandInvoker))
            {
                commandInvoker.Initialize(weaponHolder);
            }
            
            if (TryGetComponent(out HumanoidAnimatorController humanoidAnimatorController))
            {
                humanoidAnimatorController.Initialize(_health, damageController, weaponHolder, this);
            }
            
            if (TryGetComponent(out DamageableView damageableView))
            {
                damageableView.Initialize(_particleFactory, damageController);
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
            GameObject smokeParticleObject = smokeParticlePoolData.Prefab;
            
            _particleFactory.EmitParticle(smokeParticlePoolData, transform.position, smokeParticleObject.transform.rotation);
            
            OnDied?.Invoke(this);
        }

        public void SetDestination(Vector3 position)
        { }
    }
}
