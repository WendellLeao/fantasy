using Leaosoft;

namespace Fantasy.Gameplay.Tests
{
    internal sealed class HumbleEntity : Entity
    {
        protected override void OnSetUp()
        {
            base.OnSetUp();
            
            IHealth healthController = GetComponent<IHealth>();
            IDamageable damageController = GetComponent<IDamageable>();
            
            healthController.SetUp();
            damageController.SetUp(healthController);
        }
    }
}
