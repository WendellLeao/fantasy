using Leaosoft;

namespace Fantasy.Gameplay
{
    internal interface ICommandInvoker : IEntityComponent
    {
        public void SetUp(IWeaponHolder weaponHolder);
    }
}
