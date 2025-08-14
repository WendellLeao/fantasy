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
        private WeaponData _data;

        public WeaponData Data => _data;
        public string PoolId { get; set; }

        public void SetUp(WeaponData data)
        {
            _data = data;
            
            base.SetUp();
        }

        public void Execute()
        { }
        
        public void FinishExecution()
        { }
        
        public void CastSpell()
        {
            SpellData randomSpellData = spellData[Random.Range(0, spellData.Length)];
            
            _spellFactory.CastSpell(randomSpellData, spawnPoint.position, direction: transform.forward);
        }
        
        protected override void SetUpComponents()
        { }

        public void SetSpellFactory(ISpellFactory spellFactory)
        {
            _spellFactory = spellFactory;
        }
    }
}
