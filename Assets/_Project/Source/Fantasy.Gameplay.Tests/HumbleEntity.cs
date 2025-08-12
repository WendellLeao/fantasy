using Leaosoft;

namespace Fantasy.Gameplay.Tests
{
    internal sealed class HumbleEntity : Entity
    {
        protected override void InitializeComponents()
        {
            if (TryGetComponent(out IHealth healthController))
            {
                healthController.Initialize();
            }
            
            if (TryGetComponent(out IDamageable damageController))
            {
                damageController.Initialize(healthController);
            }
        }
    }
}
