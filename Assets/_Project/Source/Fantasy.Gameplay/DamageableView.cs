using Fantasy.Domain.Health;
using Leaosoft;

namespace Fantasy.Gameplay
{
    internal sealed class DamageableView : EntityComponent
    {
        private IDamageable _damageable;
        private IParticleFactory _particleFactory;
        private IParticle _cachedParticle;
        private float _damagePerSecondCountdown;
        private bool _isDamagingPerSecond;

        public void Initialize(IDamageable damageable, IParticleFactory particleFactory)
        {
            _damageable = damageable;
            _particleFactory = particleFactory;
            
            base.Initialize();
        }

        protected override void OnBegin()
        {
            base.OnBegin();
            
            _damageable.OnDamageTaken += HandleDamageTaken;
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            _damageable.OnDamageTaken -= HandleDamageTaken;
        }

        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);

            if (_isDamagingPerSecond)
            {
                TickCachedParticle(deltaTime);
            }
        }

        private void HandleDamageTaken(DamageData damageData)
        {
            if (!damageData.HasDamagePerSecond)
            {
                return;
            }

            _cachedParticle ??= _particleFactory.EmitParticle(damageData.DpsParticlePrefab, transform);

            _isDamagingPerSecond = damageData.HasDamagePerSecond;
            
            _damagePerSecondCountdown += damageData.DamagePerSecondDuration;
        }
        
        private void TickCachedParticle(float deltaTime)
        {
            _damagePerSecondCountdown -= deltaTime;

            if (_damagePerSecondCountdown <= 0f)
            {
                DisposeCachedParticle();
            }
        }
        
        private void DisposeCachedParticle()
        {
            _particleFactory.DisposeParticle(_cachedParticle);

            _cachedParticle = null;
            _isDamagingPerSecond = false;
        }
    }
}
