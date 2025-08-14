using System;
using Fantasy.Gameplay.Damage;
using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Spells
{
    internal sealed class ElementalBall : Entity, ISpell, IParticleEmitter
    {
        public event Action<ISpell> OnHit;
        
        [Header("Components")]
        [SerializeField]
        private ApplyForwardForce applyForwardForce;
        [SerializeField]
        private Damager damager;
        
        [Header("Data")]
        [SerializeField]
        private PoolData collisionParticlePoolData;
        
        private IParticleFactory _particleFactory;

        public string PoolId { get; set; }

        protected override void OnSetUp()
        {
            base.OnSetUp();
            
            applyForwardForce.SetUp();
            damager.SetUp();
            
            RegisterComponents(applyForwardForce, damager);
        }

        private void OnTriggerEnter(Collider other)
        {
            damager.TryApplyDamage(other);
            
            _particleFactory.EmitParticle(collisionParticlePoolData, transform.position, Quaternion.identity);
            
            OnHit?.Invoke(this);
        }

        public void SetParticleFactory(IParticleFactory particleFactory)
        {
            _particleFactory = particleFactory;
        }
    }
}
