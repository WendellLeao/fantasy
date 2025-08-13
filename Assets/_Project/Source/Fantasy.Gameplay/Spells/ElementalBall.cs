using System;
using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Spells
{
    internal sealed class ElementalBall : Entity, ISpell, IParticleEmitter
    {
        public event Action<ISpell> OnHit;
        
        [SerializeField]
        private PoolData collisionParticlePoolData;
        
        private IParticleFactory _particleFactory;
        private IDamager _damager;

        public string PoolId { get; set; }
        
        protected override void InitializeComponents()
        {
            if (TryGetComponent(out ApplyForwardForce applyForwardForce))
            {
                applyForwardForce.Initialize();
            }
            
            if (TryGetComponent(out _damager))
            {
                _damager.Initialize();
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            Begin();
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
