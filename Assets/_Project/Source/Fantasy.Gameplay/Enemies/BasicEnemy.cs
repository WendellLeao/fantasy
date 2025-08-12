using System;
using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Enemies
{
    public sealed class BasicEnemy : Entity, IEnemy, IPooledObject
    {
        public event Action<BasicEnemy> OnDied;
        
        [SerializeField]
        private PoolData smokeParticlePoolData;
        
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IHealth _health;

        public IHealth Health => _health;
        public string PoolId { get; set; }

        public void Initialize(IParticleFactory particleFactory, IWeaponFactory weaponFactory)
        {
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            
            base.Initialize();
        }

        protected override void InitializeComponents()
        {
            if (TryGetComponent(out _health))
            {
                _health.Initialize();
            }

            if (TryGetComponent(out IDamageable damageable))
            {
                damageable.Initialize(_health);
            }
            
            if (TryGetComponent(out IWeaponHolder weaponHolder))
            {
                weaponHolder.Initialize(_weaponFactory);
            }

            if (TryGetComponent(out ICommandInvoker commandInvoker))
            {
                commandInvoker.Initialize(weaponHolder);
            }
            
            if (TryGetComponent(out IMoveableAgent moveableAgent))
            {
                // TODO: implement this
                moveableAgent.Initialize(cameraProvider: null, particleFactory: null);
            }
            
            if (TryGetComponent(out IHumanoidAnimatorController humanoidAnimatorController))
            {
                humanoidAnimatorController.Initialize(_health, damageable, weaponHolder, moveableAgent);
            }
            
            if (TryGetComponent(out IDamageableView damageableView))
            {
                damageableView.Initialize(_particleFactory, damageable);
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
    }
}
