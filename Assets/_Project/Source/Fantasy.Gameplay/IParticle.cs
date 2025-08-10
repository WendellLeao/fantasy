using System;
using Leaosoft.Pooling;

namespace Fantasy.Gameplay
{
    public interface IParticle : IPooledObject
    {
        public event Action<IParticle> OnCompleted;

        public void Initialize();
        public void Begin();
    }
}
