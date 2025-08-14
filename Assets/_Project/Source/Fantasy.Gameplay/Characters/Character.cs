using System;
using Leaosoft;
using NaughtyAttributes;

namespace Fantasy.Gameplay.Characters
{
    public sealed class Character : Entity, ICharacter
    {
        public event Action<ICharacter> OnDied;
        
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IHealth _health;
        private IWeaponHolder _weaponHolder;
        private IMoveableAgent _navMeshClickMover;
        private ICameraProvider _cameraProvider;

        public string PoolId { get; set; }
        public IHealth Health => _health;

        public void SetUp(IParticleFactory particleFactory, IWeaponFactory weaponFactory, ICameraProvider cameraProvider)
        {
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            _cameraProvider = cameraProvider;
            
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

            if (TryGetComponent(out _weaponHolder))
            {
                _weaponHolder.SetUp(_weaponFactory);
            }

            if (TryGetComponent(out _navMeshClickMover))
            {
                _navMeshClickMover.SetUp(_cameraProvider, _particleFactory);
            }

            if (TryGetComponent(out ICommandInvoker commandInvoker))
            {
                commandInvoker.SetUp(_weaponHolder);
            }
            
            if (TryGetComponent(out IHumanoidAnimatorController humanoidAnimatorController))
            {
                humanoidAnimatorController.SetUp(_health, damageable, _weaponHolder, _navMeshClickMover);
            }
            
            if (TryGetComponent(out IDamageableView damageableView))
            {
                damageableView.SetUp(_particleFactory, damageable);
            }
        }

        protected override void OnSetUp()
        {
            base.OnSetUp();

            _cameraProvider.VirtualCamera.SetTarget(transform);
            
            _health.OnDepleted += HandleHealthDepleted;
            
            _weaponHolder.OnWeaponExecuted += HandleWeaponExecute;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _health.OnDepleted -= HandleHealthDepleted;
            
            _weaponHolder.OnWeaponExecuted -= HandleWeaponExecute;
        }

        private void HandleHealthDepleted()
        {
            OnDied?.Invoke(this);
        }
        
        private void HandleWeaponExecute()
        {
            _navMeshClickMover.ResetPath();
        }

#if UNITY_EDITOR
        [Button("SetUp_Debug")]
        public void SetUp_Debug()
        {
            SetUp(_particleFactory, _weaponFactory, _cameraProvider);
        }

        [Button("Dispose_Debug")]
        public void Dispose_Debug()
        {
            Dispose();
        }
#endif
    }
}
