using System;
using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Weapons
{
    internal sealed class Sword : Entity, IWeapon, IParticleEmitter
    {
        public event Action OnExecuted;
        
        [SerializeField]
        private CapsuleCollider capsuleCollider;
        [SerializeField]
        private GameObject bloodParticlesPrefab;
        
        private IParticleFactory _particleFactory;
        private WeaponData _data;

        public WeaponData Data => _data;

        public void Initialize(WeaponData data)
        {
            _data = data;
            
            base.Initialize();
        }
        
        public void Execute()
        {
            SetColliderEnabled(false);
            
            OnExecuted?.Invoke();
        }

        protected override void InitializeComponents()
        { }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            SetColliderEnabled(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (TryGetComponent(out Damager damager))
            {
                damager.TryApplyDamage(other);
                
                _particleFactory.EmitParticle(bloodParticlesPrefab, transform.position, Quaternion.identity);
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
