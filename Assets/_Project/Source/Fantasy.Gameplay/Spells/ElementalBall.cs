using System;
using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Spells
{
    internal sealed class ElementalBall : Entity, ISpell, IParticleEmitter
    {
        public event Action<ISpell> OnHit;
        
        [SerializeField]
        private GameObject collisionParticle;
        
        private IParticleFactory _particleFactory;
        private Damager _damager;

        public Transform Transform => transform;

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

        private void OnTriggerEnter(Collider other)
        {
            _damager.TryApplyDamage(other);
            
            _particleFactory.EmitParticle(collisionParticle, transform.position, Quaternion.identity);
            
            OnHit?.Invoke(this);
        }

        public void SetParticleFactory(IParticleFactory particleFactory)
        {
            _particleFactory = particleFactory;
        }
    }
}
