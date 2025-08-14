using System;
using Fantasy.Gameplay.Animations;
using Fantasy.Gameplay.Commands;
using Fantasy.Gameplay.Damage;
using Fantasy.Gameplay.Damage.View;
using Fantasy.Gameplay.Health;
using Fantasy.Gameplay.Navigation;
using Fantasy.Gameplay.Weapons;
using Leaosoft;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy.Gameplay.Characters
{
    public sealed class Character : Entity, ICharacter
    {
        [SerializeField]
        private HealthController healthController;
        [SerializeField]
        private DamageController damageController;
        [SerializeField]
        private WeaponHolder weaponHolder;
        [SerializeField]
        private NavMeshClickMover navMeshClickMover;
        [SerializeField]
        private CommandInputReader commandInputHeader;
        [SerializeField]
        private HumanoidAnimatorController humanoidAnimatorController;
        [SerializeField]
        private DamageableView damageableView;
        
        public event Action<ICharacter> OnDied;
        
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;
        private ICameraProvider _cameraProvider;

        public string PoolId { get; set; }
        public IHealth Health => healthController;

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

            healthController.SetUp();
            damageController.SetUp(healthController);
            weaponHolder.SetUp(_weaponFactory);
            navMeshClickMover.SetUp(_cameraProvider, _particleFactory);
            commandInputHeader.SetUp(weaponHolder);
            humanoidAnimatorController.SetUp(healthController, damageController, weaponHolder, navMeshClickMover);
            damageableView.SetUp(_particleFactory, damageController);
            
            RegisterComponents(healthController, damageController, weaponHolder, navMeshClickMover, commandInputHeader,
                humanoidAnimatorController, damageableView);
            
            _cameraProvider.VirtualCamera.SetTarget(transform);

            SubscribeEvent();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            UnsubscribeEvent();
        }

        private void SubscribeEvent()
        {
            healthController.OnDepleted += HandleHealthDepleted;
            
            weaponHolder.OnWeaponExecuted += HandleWeaponExecute;
        }
        
        private void UnsubscribeEvent()
        {
            healthController.OnDepleted -= HandleHealthDepleted;
            
            weaponHolder.OnWeaponExecuted -= HandleWeaponExecute;
        }
        
        private void HandleHealthDepleted()
        {
            OnDied?.Invoke(this);
        }
        
        private void HandleWeaponExecute()
        {
            navMeshClickMover.ResetPath();
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
