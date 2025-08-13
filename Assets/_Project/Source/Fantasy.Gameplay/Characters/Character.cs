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

        public void Initialize(IParticleFactory particleFactory, IWeaponFactory weaponFactory, ICameraProvider cameraProvider)
        {
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            _cameraProvider = cameraProvider;
            
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

            if (TryGetComponent(out _weaponHolder))
            {
                _weaponHolder.Initialize(_weaponFactory);
            }

            if (TryGetComponent(out _navMeshClickMover))
            {
                _navMeshClickMover.Initialize(_cameraProvider, _particleFactory);
            }

            if (TryGetComponent(out ICommandInvoker commandInvoker))
            {
                commandInvoker.Initialize(_weaponHolder);
            }
            
            if (TryGetComponent(out IHumanoidAnimatorController humanoidAnimatorController))
            {
                humanoidAnimatorController.Initialize(_health, damageable, _weaponHolder, _navMeshClickMover);
            }
            
            if (TryGetComponent(out IDamageableView damageableView))
            {
                damageableView.Initialize(_particleFactory, damageable);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            _cameraProvider.VirtualCamera.SetTarget(transform);
            
            Begin();
        }

        protected override void OnBegin()
        {
            base.OnBegin();

            _health.OnDepleted += HandleHealthDepleted;
            
            _weaponHolder.OnWeaponExecuted += HandleWeaponExecute;
        }

        protected override void OnStop()
        {
            base.OnStop();
            
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
