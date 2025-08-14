using System;
using Leaosoft;
using Leaosoft.Pooling;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy.Gameplay.Enemies
{
    public sealed class BasicEnemy : Entity, IEnemy
    {
        public event Action<IEnemy> OnDied;
        
        [SerializeField]
        private PoolData smokeParticlePoolData;
        
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IHealth _health;

        public string PoolId { get; set; }
        public IHealth Health =>  _health;

        public void SetUp(IParticleFactory particleFactory, IWeaponFactory weaponFactory)
        {
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            
            base.SetUp();
        }

        protected override void SetUpComponents()
        {
            if (TryGetComponent(out _health))
            {
                _health.SetUp();
            }

            if (TryGetComponent(out IDamageable damageable))
            {
                damageable.SetUp(_health);
            }
            
            if (TryGetComponent(out IWeaponHolder weaponHolder))
            {
                weaponHolder.SetUp(_weaponFactory);
            }

            if (TryGetComponent(out ICommandInvoker commandInvoker))
            {
                commandInvoker.SetUp(weaponHolder);
            }
            
            if (TryGetComponent(out IMoveableAgent moveableAgent))
            {
                // TODO: implement this
                moveableAgent.SetUp(cameraProvider: null, particleFactory: null);
            }
            
            if (TryGetComponent(out IHumanoidAnimatorController humanoidAnimatorController))
            {
                humanoidAnimatorController.SetUp(_health, damageable, weaponHolder, moveableAgent);
            }
            
            if (TryGetComponent(out IDamageableView damageableView))
            {
                damageableView.SetUp(_particleFactory, damageable);
            }
        }

        protected override void OnSetUp()
        {
            base.OnSetUp();

            _health.OnDepleted += HandleHealthDepleted;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _health.OnDepleted -= HandleHealthDepleted;
        }

        private void HandleHealthDepleted()
        {
            GameObject smokeParticleObject = smokeParticlePoolData.Prefab;
            
            _particleFactory.EmitParticle(smokeParticlePoolData, transform.position, smokeParticleObject.transform.rotation);
            
            OnDied?.Invoke(this);
        }
        
#if UNITY_EDITOR
        [Button("SetUp_Debug")]
        public void SetUp_Debug()
        {
            SetUp(_particleFactory, _weaponFactory);
        }

        [Button("Dispose_Debug")]
        public void Dispose_Debug()
        {
            Dispose();
        }
#endif
    }
}
