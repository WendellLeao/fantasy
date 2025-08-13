namespace Fantasy.Gameplay
{
    internal interface IMeleeWeapon : IWeapon, IParticleEmitter
    {
        public void SetColliderEnabled(bool isEnabled);
    }
}
