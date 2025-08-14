using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Weapons
{
    internal sealed class Sword : Entity, IMeleeWeapon
    {
        [SerializeField]
        private CapsuleCollider capsuleCollider;
        [SerializeField]
        private PoolData bloodParticlesPoolData;
        
        private IParticleFactory _particleFactory;
        private WeaponData _data;
        private IDamager _damager;

        public WeaponData Data => _data;
        public string PoolId { get; set; }

        public void SetUp(WeaponData data)
        {
            _data = data;
            
            base.SetUp();
        }
        
        public void Execute()
        {
            SetColliderEnabled(false);
        }

        public void FinishExecution()
        {
            SetColliderEnabled(false);
        }

        protected override void SetUpComponents()
        {
            if (TryGetComponent(out _damager))
            {
                _damager.SetUp();
            }
        }

        protected override void OnSetUp()
        {
            base.OnSetUp();
            
            SetColliderEnabled(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsEnabled)
            {
                return;
            }
            
            _damager.TryApplyDamage(other);
            
            _particleFactory.EmitParticle(bloodParticlesPoolData, transform.position, Quaternion.identity);
        }

        public void SetColliderEnabled(bool isEnabled)
        {
            capsuleCollider.enabled = isEnabled;
        }
        
        public void SetParticleFactory(IParticleFactory particleFactory)
        {
            _particleFactory = particleFactory;
        }
    }
}
