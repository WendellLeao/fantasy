using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Weapons
{
    internal sealed class Sword : Entity, IMeleeWeapon, IParticleEmitter
    {
        [SerializeField]
        private CapsuleCollider capsuleCollider;
        [SerializeField]
        private PoolData bloodParticlesPoolData;
        
        private IParticleFactory _particleFactory;
        private WeaponData _data;

        public WeaponData Data => _data;
        public string PoolId { get; set; }

        public void Initialize(WeaponData data)
        {
            _data = data;
            
            base.Initialize();
        }
        
        public void Execute()
        {
            SetColliderEnabled(false);
        }

        public void FinishExecution()
        {
            SetColliderEnabled(false);
        }

        protected override void InitializeComponents()
        {
            if (TryGetComponent(out Damager damager))
            {
                damager.Initialize();
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            SetColliderEnabled(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsEnabled)
            {
                return;
            }
            
            if (TryGetComponent(out Damager damager))
            {
                damager.TryApplyDamage(other);
                
                _particleFactory.EmitParticle(bloodParticlesPoolData, transform.position, Quaternion.identity);
            }
        }

        public void SetParticleFactory(IParticleFactory particleFactory)
        {
            _particleFactory = particleFactory;
        }

        public void SetColliderEnabled(bool isEnabled)
        {
            capsuleCollider.enabled = isEnabled;
        }
    }
}
