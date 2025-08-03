using System;
using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Particles.Manager
{
    public sealed class ParticleManager : Leaosoft.Manager, IParticleFactory
    {
        public IParticle EmitParticle(GameObject prefab, Transform parent)
        {
            IEntity entity = CreateEntity(prefab, parent);

            entity.Initialize();
            entity.Begin();
            
            if (entity is not IParticle particle)
            {
                throw new InvalidOperationException($"Wasn't possible to cast the '{entity}' to '{nameof(IParticle)}'");
            }

            particle.OnCompleted += DisposeParticle;
            
            return particle;
        }
        
        public IParticle EmitParticle(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            IParticle particle = EmitParticle(prefab, parent: null);

            particle.Transform.SetPositionAndRotation(position, rotation);
            
            return particle;
        }
        
        public void DisposeParticle(IParticle particle)
        {
            particle.OnCompleted -= DisposeParticle;
            
            DisposeEntity(particle as IEntity);
            
            Destroy(particle.Transform.gameObject); // TODO: use pooling
        }
    }
}
