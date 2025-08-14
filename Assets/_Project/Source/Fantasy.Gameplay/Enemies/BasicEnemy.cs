using System;
using Fantasy.Gameplay.Animations;
using Fantasy.Gameplay.Commands;
using Fantasy.Gameplay.Damage;
using Fantasy.Gameplay.Damage.View;
using Fantasy.Gameplay.Health;
using Fantasy.Gameplay.Navigation;
using Fantasy.Gameplay.Weapons;
using Leaosoft;
using Leaosoft.Pooling;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy.Gameplay.Enemies
{
    public sealed class BasicEnemy : Entity, IEnemy
    {
        public event Action<IEnemy> OnDied;
        
        [Header("Components")]
        [SerializeField]
        private HealthController healthController;
        [SerializeField]
        private DamageController damageController;
        [SerializeField]
        private WeaponHolder weaponHolder;
        [SerializeField]
        private NavMeshTest navMeshTest;
        [SerializeField]
        private CommandAutoInvoker commandAutoInvoker;
        [SerializeField]
        private HumanoidAnimatorController humanoidAnimatorController;
        [SerializeField]
        private DamageableView damageableView;
        
        [Header("Data")]
        [SerializeField]
        private PoolData smokeParticlePoolData;
        
        private IParticleFactory _particleFactory;
        private IWeaponFactory _weaponFactory;

        public string PoolId { get; set; }
        public IHealth Health =>  healthController;

        public void SetUp(IParticleFactory particleFactory, IWeaponFactory weaponFactory)
        {
            _particleFactory = particleFactory;
            _weaponFactory = weaponFactory;
            
            base.SetUp();
        }

        protected override void OnSetUp()
        {
            base.OnSetUp();

            healthController.SetUp();
            damageController.SetUp(healthController);
            weaponHolder.SetUp(_weaponFactory);
            commandAutoInvoker.SetUp(weaponHolder);
            navMeshTest.SetUp(cameraProvider: null, particleFactory: null); // TODO: implement this
            humanoidAnimatorController.SetUp(healthController, damageController, weaponHolder, navMeshTest);
            damageableView.SetUp(_particleFactory, damageController);

            RegisterComponents(healthController, damageController, weaponHolder, navMeshTest, commandAutoInvoker,
                humanoidAnimatorController, damageableView);
            
            healthController.OnDepleted += HandleHealthDepleted;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            healthController.OnDepleted -= HandleHealthDepleted;
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
