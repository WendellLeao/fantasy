using System;
using Leaosoft;
using Leaosoft.Pooling;

namespace Fantasy.Gameplay
{
    public interface ISpell : IEntity, IPooledObject
    {
        public event Action<ISpell> OnHit;
    }
}
