namespace Fantasy.Gameplay
{
    internal interface IDamageableView
    {
        public void SetUp(IParticleFactory particleFactory, IDamageable damageable);
    }
}
