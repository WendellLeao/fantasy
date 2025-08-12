using System;
using Leaosoft;

namespace Fantasy.Gameplay
{
    public interface IEnemy : IEntity
    {
        public event Action<IEnemy> OnDied;

        public void Initialize(IParticleFactory particleFactory, IWeaponFactory weaponFactory);
    }
}
