using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay
{
    public interface IParticleFactory
    {
        public IParticle EmitParticle(PoolData particlePoolData, Transform parent);
        public IParticle EmitParticle(PoolData particlePoolData, Vector3 position, Quaternion rotation);
        public virtual void DisposeEntity(IParticle particle) { }
    }
}
