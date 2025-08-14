using System;

namespace Fantasy.Gameplay
{
    public interface IDamageable
    {
        public event Action<DamageData> OnDamageTaken;

        public void SetUp(IHealth health);
        public void TakeDamage(DamageData damageData);
        public void SetIsInvincible(bool isInvincible);
    }
}
