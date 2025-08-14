namespace Fantasy.Gameplay
{
    internal interface IHumanoidAnimatorController
    {
        public void SetUp(IHealth health, IDamageable damageable, IWeaponHolder weaponHolder,
            IMoveableAgent moveableAgent);
    }
}
