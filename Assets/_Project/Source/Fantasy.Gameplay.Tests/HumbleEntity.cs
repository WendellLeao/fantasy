using Leaosoft;

namespace Fantasy.Gameplay.Tests
{
    internal sealed class HumbleEntity : Entity
    {
        protected override void SetUpComponents()
        {
            if (TryGetComponent(out IHealth healthController))
            {
                healthController.SetUp();
            }
            
            if (TryGetComponent(out IDamageable damageController))
            {
                damageController.SetUp(healthController);
            }
        }
    }
}
