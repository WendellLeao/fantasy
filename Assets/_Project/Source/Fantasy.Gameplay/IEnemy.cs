using System;
using Leaosoft;
using Leaosoft.Pooling;

namespace Fantasy.Gameplay
{
    public interface IEnemy : IEntity, IPooledObject
    {
        public event Action<IEnemy> OnDied;

        public IHealth Health { get; }
        
        public void Initialize(IParticleFactory particleFactory, IWeaponFactory weaponFactory);
    }
}
