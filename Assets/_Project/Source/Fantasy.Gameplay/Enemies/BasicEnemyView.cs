using Fantasy.Domain.Health;
using Leaosoft;

namespace Fantasy.Gameplay.Enemies
{
    internal sealed class BasicEnemyView : EntityView
    {
        private IParticleFactory _particleFactory;
        private IHealth _health;
        private IDamageable _damageable;
        private IWeaponHolder _weaponHolder;
        private IMoveableAgent _moveableAgent;

        public void Initialize(IParticleFactory particleFactory, IHealth health, IDamageable damageable, IWeaponHolder weaponHolder,
            IMoveableAgent moveableAgent)
        {
            _particleFactory = particleFactory;
            _health = health;
            _damageable = damageable;
            _weaponHolder = weaponHolder;
            _moveableAgent = moveableAgent;
            
            base.Initialize();
        }

        protected override void InitializeComponents()
        {
            if (TryGetComponent(out HumanoidAnimatorController humanoidAnimatorController))
            {
                humanoidAnimatorController.Initialize(_health, _damageable, _weaponHolder, _moveableAgent);
            }
            
            if (TryGetComponent(out DamageableView damageableView))
            {
                damageableView.Initialize(_particleFactory, _damageable);
            }
        }
    }
}
