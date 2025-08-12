using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Particles.Manager
{
    public sealed class ParticleManager : Leaosoft.Manager, IParticleFactory
    {
        private IPoolingService _poolingService;

        public void Initialize(IPoolingService poolingService)
        {
            _poolingService = poolingService;
            
            base.Initialize();
        }
        
        public IParticle EmitParticle(PoolData particlePoolData, Transform parent)
        {
            if (!_poolingService.TryGetObjectFromPool(particlePoolData.Id, out IParticle particle))
            {
                return null;
            }

            AddEntity(particle as Entity);
            
            particle.GameObject.transform.SetParent(parent, worldPositionStays: false);
            
            particle.Initialize();
            particle.Begin();
            
            particle.OnCompleted += DisposeParticle;
            
            return particle;
        }
        
        public IParticle EmitParticle(PoolData particlePoolData, Vector3 position, Quaternion rotation)
        {
            IParticle particle = EmitParticle(particlePoolData, parent: null);

            Transform particleTransform = particle.GameObject.transform;
            
            particleTransform.SetPositionAndRotation(position, rotation);
            
            return particle;
        }
        
        public void DisposeParticle(IParticle particle)
        {
            DisposeEntity(particle as Entity);
        }

        protected override void DisposeEntity(Entity entity)
        {
            base.DisposeEntity(entity);

            if (entity is not IParticle particle)
            {
                return;
            }
            
            particle.OnCompleted -= DisposeParticle;
            
            _poolingService.ReleaseObjectToPool(particle);
        }
    }
}
