using System;
using Leaosoft;
using Leaosoft.Pooling;

namespace Fantasy.Gameplay
{
    public interface IParticle : IEntity, IPooledObject
    {
        public event Action<IParticle> OnCompleted;
    }
}
