using Leaosoft;

namespace Fantasy.Gameplay
{
    internal interface IDamageableView : IEntityComponent
    {
        public void SetUp(IParticleFactory particleFactory, IDamageable damageable);
    }
}
