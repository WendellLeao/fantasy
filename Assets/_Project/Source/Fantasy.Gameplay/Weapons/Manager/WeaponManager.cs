using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Weapons.Manager
{
    public sealed class WeaponManager : EntityManager<IWeapon>, IWeaponFactory
    {
        private IPoolingService _poolingService;
        private IParticleFactory _particleFactory;
        private ISpellFactory _spellFactory;
        
        public void Initialize(IPoolingService poolingService, IParticleFactory particleFactory, ISpellFactory spellFactory)
        {
            _poolingService = poolingService;
            _particleFactory = particleFactory;
            _spellFactory = spellFactory;
            
            base.Initialize();
        }
        
        public IWeapon CreateWeapon(WeaponData data, Transform parent)
        {
            if (!_poolingService.TryGetObjectFromPool(data.PoolData.Id, parent, out IWeapon weapon))
            {
                return null;
            }
            
            RegisterEntity(weapon);

            weapon.SetUp(data);
            
            if (weapon is IParticleEmitter particleEmitter)
            {
                particleEmitter.SetParticleFactory(_particleFactory);
            }
            
            if (weapon is ISpellCaster spellCaster)
            {
                spellCaster.SetSpellFactory(_spellFactory);
            }
            
            return weapon;
        }

        public void DisposeWeapon(IWeapon weapon)
        {
            DisposeEntity(weapon);
        }

        protected override void DisposeEntity(IWeapon weapon)
        {
            base.DisposeEntity(weapon);
            
            _poolingService.ReleaseObjectToPool(weapon);
        }
    }
}
