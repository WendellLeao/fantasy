using System;
using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Spells.Manager
{
    internal sealed class SpellManager : Leaosoft.Manager, ISpellFactory
    {
        private IParticleFactory _particleFactory;

        public void Initialize(IParticleFactory particleFactory)
        {
            _particleFactory = particleFactory;
            
            base.Initialize();
        }
        
        public ISpell CastSpell(SpellData data, Vector3 position, Vector3 direction)
        {
            IEntity entity = CreateEntity(data.Prefab, parent: null);
            
            entity.Initialize();
            entity.Begin();

            if (entity is not ISpell spell)
            {
                throw new InvalidOperationException($"Wasn't possible to cast the '{entity}' to '{nameof(ISpell)}'");
            }
            
            spell.Transform.SetPositionAndRotation(position, Quaternion.LookRotation(direction));
            
            spell.OnHit += HandleSpellHit;
            
            if (spell is IParticleEmitter particleEmitter)
            {
                particleEmitter.SetParticleFactory(_particleFactory);
            }
            
            return spell;
        }
        
        private void HandleSpellHit(ISpell spell)
        {
            spell.OnHit -= HandleSpellHit;
                
            DisposeEntity(spell as IEntity);
            
            Destroy(spell.Transform.gameObject); // TODO: use pooling
        }
    }
}
