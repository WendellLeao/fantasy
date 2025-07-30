using UnityEngine;

namespace Fantasy.Gameplay
{
    public interface IParticleFactory
    {
        public IParticle EmitParticle(GameObject prefab, Transform parent);
        public IParticle EmitParticle(GameObject prefab, Vector3 position, Quaternion rotation);
        public void DisposeParticle(IParticle particle);
    }
}
