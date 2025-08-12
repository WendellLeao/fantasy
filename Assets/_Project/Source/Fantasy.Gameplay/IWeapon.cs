using Leaosoft.Pooling;

namespace Fantasy.Gameplay
{
    public interface IWeapon : IPooledObject
    {
        public WeaponData Data { get; }
        
        public void Initialize(WeaponData data);
        public void Begin();
        public void Stop();
        public void Execute();
        public void FinishExecution();
    }
}
