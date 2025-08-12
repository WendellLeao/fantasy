using System;
using Leaosoft;
using Leaosoft.Pooling;
using NaughtyAttributes;

namespace Fantasy.Gameplay.Characters
{
    internal sealed class Character : Entity, ICharacter, IPooledObject
    {
        public event Action<Character> OnDied;
        
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private IHealth _health;
        private WeaponHolder _weaponHolder;
        private NavMeshClickMover _navMeshClickMover;
        private ICameraProvider _cameraProvider;

        public IHealth Health => _health;
        public string PoolId { get; set; }
        
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

                _health = healthController;
            }
            
            if (TryGetComponent(out DamageController damageController))
            {
                damageController.Initialize(_health);
            }

            if (TryGetComponent(out _weaponHolder))
            {
                _weaponHolder.Initialize(_weaponFactory);
            }

            if (TryGetComponent(out _navMeshClickMover))
            {
                _navMeshClickMover.Initialize(_cameraProvider, _particleFactory);
            }

            if (TryGetComponent(out CommandInputReader commandReader))
            {
                commandReader.Initialize(_weaponHolder);
            }
            
            if (TryGetComponent(out HumanoidAnimatorController humanoidAnimatorController))
            {
                humanoidAnimatorController.Initialize(_health, damageController, _weaponHolder, _navMeshClickMover);
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
