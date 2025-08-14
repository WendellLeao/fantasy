using Fantasy.Gameplay.Damage;
using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Weapons
{
    internal sealed class Sword : Entity, IMeleeWeapon
    {
        [Header("Components")]
        [SerializeField]
        private CapsuleCollider capsuleCollider;
        [SerializeField]
        private Damager damager;
        
        [Header("Data")]
        [SerializeField]
        private PoolData bloodParticlesPoolData;
        
        private IParticleFactory _particleFactory;
        private WeaponData _data;

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

        protected override void OnSetUp()
        {
            base.OnSetUp();
            
            damager.SetUp();
            
            RegisterComponents(damager);
            
            SetColliderEnabled(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsEnabled)
            {
                return;
            }
            
            damager.TryApplyDamage(other);
            
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
