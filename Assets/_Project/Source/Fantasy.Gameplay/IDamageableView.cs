namespace Fantasy.Gameplay
{
    internal interface IDamageableView
    {
        public void Initialize(IParticleFactory particleFactory, IDamageable damageable);
    }
}
