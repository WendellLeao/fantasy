using System;
using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Spells
{
    internal sealed class Heal : Entity, ISpell
    {
        public event Action<ISpell> OnHit;

        public Transform Transform => transform;

        protected override void InitializeComponents()
        { }
    }
}
