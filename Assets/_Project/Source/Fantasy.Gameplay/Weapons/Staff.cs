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
        [SerializeField]
        private BoxCollider boxCollider;

        private ISpellFactory _spellFactory;
        private WeaponData _data;

        public WeaponData Data => _data;

        public void Initialize(WeaponData data)
        {
            _data = data;
            
            base.Initialize();
        }
        
        public void Execute()
        {
            SpellData randomSpellData = spellData[Random.Range(0, spellData.Length)];
            
            _spellFactory.CastSpell(randomSpellData, spawnPoint.position, direction: transform.forward);
        }
        
        protected override void InitializeComponents()
        { }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            SetColliderEnabled(false);
        }

        public void SetSpellFactory(ISpellFactory spellFactory)
        {
            _spellFactory = spellFactory;
        }

        public void SetColliderEnabled(bool isEnabled)
        {
            boxCollider.enabled = isEnabled;
        }
    }
}
