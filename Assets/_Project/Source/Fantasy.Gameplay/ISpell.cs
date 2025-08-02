using System;
using UnityEngine;

namespace Fantasy.Gameplay
{
    internal interface ISpell
    {
        public event Action<ISpell> OnHit;
        
        public Transform Transform { get; }
    }
}
