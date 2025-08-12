using System;
using Leaosoft;

namespace Fantasy.Gameplay
{
    public interface ICharacter : IEntity
    {
        public event Action<ICharacter> OnDied;
        
        public void Initialize(IParticleFactory particleFactory, IWeaponFactory weaponFactory,
            ICameraProvider cameraProvider);
    }
}
