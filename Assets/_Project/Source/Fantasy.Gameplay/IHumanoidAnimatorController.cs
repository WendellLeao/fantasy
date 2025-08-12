namespace Fantasy.Gameplay
{
    internal interface IHumanoidAnimatorController
    {
        public void Initialize(IHealth health, IDamageable damageable, IWeaponHolder weaponHolder,
            IMoveableAgent moveableAgent);
    }
}
