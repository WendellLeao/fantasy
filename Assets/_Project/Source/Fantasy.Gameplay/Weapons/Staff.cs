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

        public void Initialize(WeaponData data)
        {
            _data = data;
            
            base.Initialize();
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
        
        protected override void InitializeComponents()
        { }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            Begin();
        }

        public void SetSpellFactory(ISpellFactory spellFactory)
        {
            _spellFactory = spellFactory;
        }
    }
}
