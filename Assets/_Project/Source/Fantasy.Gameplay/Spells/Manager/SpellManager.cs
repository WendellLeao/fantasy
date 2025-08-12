using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Spells.Manager
{
    internal sealed class SpellManager : Leaosoft.Manager, ISpellFactory
    {
        private IPoolingService _poolingService;
        private IParticleFactory _particleFactory;

        public void Initialize(IPoolingService poolingService, IParticleFactory particleFactory)
        {
            _poolingService = poolingService;
            _particleFactory = particleFactory;
            
            base.Initialize();
        }
        
        public ISpell CastSpell(SpellData data, Vector3 position, Vector3 direction)
        {
            if (!_poolingService.TryGetObjectFromPool(data.PoolData.Id, out ISpell spell))
            {
                return null;
            }
            
            AddEntity(spell as Entity);

            spell.Initialize();
            spell.Begin();

            SetSpellPositionAndRotation(position, direction, spell);

            spell.OnHit += HandleSpellHit;
            
            if (spell is IParticleEmitter particleEmitter)
            {
                particleEmitter.SetParticleFactory(_particleFactory);
            }
            
            return spell;
        }

        protected override void DisposeEntity(Entity entity)
        {
            base.DisposeEntity(entity);

            if (entity is not ISpell spell)
            {
                return;  
            }
            
            spell.OnHit -= HandleSpellHit;
            
            _poolingService.ReleaseObjectToPool(spell);
        }

        private void HandleSpellHit(ISpell spell)
        {
            DisposeEntity(spell as Entity);
        }
        
        private void SetSpellPositionAndRotation(Vector3 position, Vector3 direction, ISpell spell)
        {
            Transform spellTransform = spell.GameObject.transform;

            spellTransform.SetPositionAndRotation(position, Quaternion.LookRotation(direction));
        }
    }
}
