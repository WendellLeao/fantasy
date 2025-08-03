using System;
using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Weapons.Manager
{
    public sealed class WeaponManager : Leaosoft.Manager, IWeaponFactory
    {
        private IParticleFactory _particleFactory;
        private ISpellFactory _spellFactory;

        public void Initialize(IParticleFactory particleFactory, ISpellFactory spellFactory)
        {
            _particleFactory = particleFactory;
            _spellFactory = spellFactory;
            
            base.Initialize();
        }
        
        public IWeapon CreateWeapon(WeaponData data, Transform parent)
        {
            IEntity entity = CreateEntity(data.Prefab, parent);

            if (entity is not IWeapon weapon)
            {
                throw new InvalidOperationException($"Wasn't possible to cast the '{entity}' to '{nameof(IWeapon)}'");
            }
            
            weapon.Initialize(data);
            weapon.Begin();
            
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
            if (weapon is IEntity entity)
            {
                DisposeEntity(entity);
                
                Destroy(entity.GameObject); // TODO: use pooling
            }
        }
    }
}
