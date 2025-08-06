using Fantasy.Domain.Health;
using Fantasy.Domain.Weapons;
using Leaosoft;

namespace Fantasy.Gameplay.Characters
{
    public sealed class CharacterView : EntityView
    {
        private IMoveableAgent _moveableAgent;
        private IDamageable _damageable;
        private IWeaponHolder _weaponHolder;
        private IParticleFactory _particleFactory;

        public void Initialize(IMoveableAgent moveableAgent, IDamageable damageable, IWeaponHolder weaponHolder, IParticleFactory particleFactory)
        {
            _moveableAgent = moveableAgent;
            _damageable = damageable;
            _weaponHolder = weaponHolder;
            _particleFactory = particleFactory;
            
            base.Initialize();
        }

        protected override void InitializeComponents()
        {
            if (TryGetComponent(out HumanoidAnimatorController humanoidAnimatorController))
            {
                humanoidAnimatorController.Initialize(_moveableAgent, _damageable, _weaponHolder);
            }
            
            if (TryGetComponent(out DamageableView damageableView))
            {
                damageableView.Initialize(_damageable, _particleFactory);
            }
        }
    }
}
