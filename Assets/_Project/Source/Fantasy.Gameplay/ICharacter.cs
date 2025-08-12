using Leaosoft;

namespace Fantasy.Gameplay
{
    public interface ICharacter : IEntity
    {
        public void Initialize(IParticleFactory particleFactory, IWeaponFactory weaponFactory,
            ICameraProvider cameraProvider);
    }
}
