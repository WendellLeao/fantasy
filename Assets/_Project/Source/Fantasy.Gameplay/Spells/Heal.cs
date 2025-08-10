using System;
using Leaosoft;

namespace Fantasy.Gameplay.Spells
{
    internal sealed class Heal : Entity, ISpell
    {
        public event Action<ISpell> OnHit;

        public string PoolId { get; set; }
        
        protected override void InitializeComponents()
        { }
    }
}
