using Leaosoft;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fantasy.Gameplay.Weapons
{
    internal sealed class Staff : Entity, IWeapon, ISpellCaster
    {
        [SerializeField]
        private SpellData[] spellData;
        [SerializeField]
        private Transform spawnPoint;

        private ISpellFactory _spellFactory;
        
        public void Trigger()
        {
            SpellData randomSpellData = spellData[Random.Range(0, spellData.Length)];
            
            _spellFactory.CastSpell(randomSpellData, spawnPoint.position, direction: transform.forward);
        }
        
        public void SetSpellFactory(ISpellFactory spellFactory)
        {
            _spellFactory = spellFactory;
        }

        protected override void InitializeComponents()
        { }
    }
}
