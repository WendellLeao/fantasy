using System;
using Leaosoft;
using Leaosoft.Pooling;

namespace Fantasy.Gameplay
{
    public interface ICharacter : IEntity, IPooledObject
    {
        public event Action<ICharacter> OnDied;
        
        public IHealth Health { get; }
        
        public void Initialize(IParticleFactory particleFactory, IWeaponFactory weaponFactory,
            ICameraProvider cameraProvider);
    }
}
