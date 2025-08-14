using Leaosoft;

namespace Fantasy.Gameplay
{
    internal interface IHumanoidAnimatorController : IEntityComponent
    {
        public void SetUp(IHealth health, IDamageable damageable, IWeaponHolder weaponHolder,
            IMoveableAgent moveableAgent);
    }
}
