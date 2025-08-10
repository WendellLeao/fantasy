using System;
using Leaosoft.Pooling;

namespace Fantasy.Gameplay
{
    public interface ISpell : IPooledObject
    {
        public event Action<ISpell> OnHit;

        public void Initialize();
        public void Begin();
    }
}
