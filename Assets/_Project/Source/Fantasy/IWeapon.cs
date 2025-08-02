namespace Fantasy
{
    public interface IWeapon
    {
        public WeaponData Data { get; }
        
        public void Initialize(WeaponData data);
        public void Begin();
        public void Execute();
    }
}
