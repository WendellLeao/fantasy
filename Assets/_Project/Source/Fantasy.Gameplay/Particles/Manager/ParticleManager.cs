using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Particles.Manager
{
    public sealed class ParticleManager : EntityManager<IParticle>, IParticleFactory
    {
        private IPoolingService _poolingService;
        
        public void Initialize(IPoolingService poolingService)
        {
            _poolingService = poolingService;
            
            base.Initialize();
        }
        
        public IParticle EmitParticle(PoolData particlePoolData, Transform parent)
        {
            if (!_poolingService.TryGetObjectFromPool(particlePoolData.Id, parent, out IParticle particle))
            {
                return null;
            }

            RegisterEntity(particle);
            
            particle.SetUp();
            
            particle.OnCompleted += DisposeEntity;
            
            return particle;
        }
        
        public IParticle EmitParticle(PoolData particlePoolData, Vector3 position, Quaternion rotation)
        {
            IParticle particle = EmitParticle(particlePoolData, parent: null);

            particle.transform.SetPositionAndRotation(position, rotation);
            
            return particle;
        }

        public void DisposeParticle(IParticle particle)
        {
            DisposeEntity(particle);
        }

        protected override void DisposeEntity(IParticle particle)
        {
            base.DisposeEntity(particle);

            particle.OnCompleted -= DisposeEntity;
            
            _poolingService.ReleaseObjectToPool(particle);
        }
    }
}
