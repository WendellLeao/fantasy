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
        private ICameraProvider _cameraProvider;
        private IHealth _health;
        private IDamageable _damageable;
        private IWeaponHolder _weaponHolder;
        private IMoveableAgent _moveableAgent;
        private ICommandInvoker _commandInvoker;
        private IHumanoidAnimatorController _humanoidAnimatorController;
        private IDamageableView _damageableView;

        public string PoolId { get; set; }
        public IHealth Health => _health;

        public void SetUp(IParticleFactory particleFactory, IWeaponFactory weaponFactory, ICameraProvider cameraProvider)
        {
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            _cameraProvider = cameraProvider;
            
            base.SetUp();
        }

        protected override void OnSetUp()
        {
            base.OnSetUp();

            CacheComponents();
            
            SetUpComponents();
            
            RegisterComponents(_health, _damageable, _weaponHolder, _moveableAgent, _commandInvoker,
                _humanoidAnimatorController, _damageableView);
            
            _cameraProvider.VirtualCamera.SetTarget(transform);

            SubscribeEvent();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            UnsubscribeEvent();
        }

        private void CacheComponents()
        {
            _health = GetComponent<IHealth>();
            _damageable = GetComponent<IDamageable>();
            _weaponHolder = GetComponent<IWeaponHolder>();
            _moveableAgent = GetComponent<IMoveableAgent>();
            _commandInvoker = GetComponent<ICommandInvoker>();
            _humanoidAnimatorController = GetComponent<IHumanoidAnimatorController>();
            _damageableView = GetComponent<IDamageableView>();
        }

        private void SetUpComponents()
        {
            _health.SetUp();
            _damageable.SetUp(_health);
            _weaponHolder.SetUp(_weaponFactory);
            _moveableAgent.SetUp(_cameraProvider, _particleFactory);
            _commandInvoker.SetUp(_weaponHolder);
            _humanoidAnimatorController.SetUp(_health, _damageable, _weaponHolder, _moveableAgent);
            _damageableView.SetUp(_particleFactory, _damageable);
        }
        
        private void SubscribeEvent()
        {
            _health.OnDepleted += HandleHealthDepleted;
            
            _weaponHolder.OnWeaponExecuted += HandleWeaponExecute;
        }
        
        private void UnsubscribeEvent()
        {
            _health.OnDepleted -= HandleHealthDepleted;
            
            _weaponHolder.OnWeaponExecuted -= HandleWeaponExecute;
        }
        
        private void HandleHealthDepleted()
        {
            OnDied?.Invoke(this);
        }
        
        private void HandleWeaponExecute()
        {
            _moveableAgent.ResetPath();
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
