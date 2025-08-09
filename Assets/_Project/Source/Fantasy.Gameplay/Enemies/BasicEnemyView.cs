using Fantasy.Domain.Health;
using Fantasy.Domain.Weapons;
using Leaosoft;

namespace Fantasy.Gameplay.Enemies
{
    internal sealed class BasicEnemyView : EntityView
    {
        private IMoveableAgent _moveableAgent;
        private IHealth _health;
        private IDamageable _damageable;
        private IWeaponHolder _weaponHolder;
        private IParticleFactory _particleFactory;

        public void Initialize(IMoveableAgent moveableAgent, IHealth health, IDamageable damageable, IWeaponHolder weaponHolder,
            IParticleFactory particleFactory)
        {
            _moveableAgent = moveableAgent;
            _health = health;
            _damageable = damageable;
            _weaponHolder = weaponHolder;
            _particleFactory = particleFactory;
            
            base.Initialize();
        }

        protected override void InitializeComponents()
        {
            if (TryGetComponent(out HumanoidAnimatorController humanoidAnimatorController))
            {
                humanoidAnimatorController.Initialize(_moveableAgent, _health, _damageable, _weaponHolder);
            }
            
            if (TryGetComponent(out DamageableView damageableView))
            {
                damageableView.Initialize(_damageable, _particleFactory);
            }
        }
    }
}
