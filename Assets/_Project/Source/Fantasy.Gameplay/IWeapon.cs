using Leaosoft;
using Leaosoft.Pooling;

namespace Fantasy.Gameplay
{
    public interface IWeapon : IEntity, IPooledObject
    {
        public WeaponData Data { get; }
        
        public void Initialize(WeaponData data);
        public void Execute();
        public void FinishExecution();
    }
}
