using Fantasy.Commands;

namespace Fantasy.Gameplay.Commands
{
    internal sealed class AttackCommand : ICommand
    {
        private readonly IWeaponHolder _weaponHolder;

        public AttackCommand(IWeaponHolder weaponHolder)
        {
            _weaponHolder = weaponHolder;
        }

        public void Execute()
        {
            _weaponHolder.ExecuteWeapon();
        }
    }
}
