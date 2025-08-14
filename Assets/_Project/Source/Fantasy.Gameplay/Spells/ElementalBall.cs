using System;
using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Spells
{
    internal sealed class ElementalBall : Entity, ISpell, IParticleEmitter
    {
        public event Action<ISpell> OnHit;
        
        [Header("Data")]
        [SerializeField]
        private PoolData collisionParticlePoolData;
        
        private IParticleFactory _particleFactory;
        private ApplyForwardForce _applyForwardForce;
        private IDamager _damager;

        public string PoolId { get; set; }

        protected override void OnSetUp()
        {
            base.OnSetUp();

            _applyForwardForce = GetComponent<ApplyForwardForce>();
            _damager = GetComponent<IDamager>();
            
            _applyForwardForce.SetUp();
            _damager.SetUp();
            
            RegisterComponents(_applyForwardForce, _damager);
        }

        private void OnTriggerEnter(Collider other)
        {
            _damager.TryApplyDamage(other);
            
            _particleFactory.EmitParticle(collisionParticlePoolData, transform.position, Quaternion.identity);
            
            OnHit?.Invoke(this);
        }

        public void SetParticleFactory(IParticleFactory particleFactory)
        {
            _particleFactory = particleFactory;
        }
    }
}
