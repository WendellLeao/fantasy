using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay.Spells.Manager
{
    internal sealed class SpellManager : EntityManager<ISpell>, ISpellFactory
    {
        private IPoolingService _poolingService;
        private IParticleFactory _particleFactory;

        public override void DisposeEntity(ISpell spell)
        {
            base.DisposeEntity(spell);

            spell.OnHit -= DisposeEntity;
            
            _poolingService.ReleaseObjectToPool(spell);
        }
        
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
            
            RegisterEntity(spell);

            spell.Initialize();

            SetSpellPositionAndRotation(position, direction, spell);

            spell.OnHit += DisposeEntity;
            
            if (spell is IParticleEmitter particleEmitter)
            {
                particleEmitter.SetParticleFactory(_particleFactory);
            }
            
            return spell;
        }

        private void SetSpellPositionAndRotation(Vector3 position, Vector3 direction, ISpell spell)
        {
            Transform spellTransform = ((IPooledObject)spell).gameObject.transform;

            spellTransform.SetPositionAndRotation(position, Quaternion.LookRotation(direction));
        }
    }
}
