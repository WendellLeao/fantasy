using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Weapons
{
    internal sealed class Sword : Entity, IWeapon, IParticleEmitter
    {
        [SerializeField]
        private GameObject bloodParticlesPrefab;
        
        private IParticleFactory _particleFactory;

        public void Trigger()
        {
            Debug.Log("<color=cyan>Swing the sword!</color>");
        }

        protected override void InitializeComponents()
        { }

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
    }
}
