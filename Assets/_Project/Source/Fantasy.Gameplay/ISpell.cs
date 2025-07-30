using System;
using UnityEngine;

namespace Fantasy.Gameplay
{
    public interface ISpell
    {
        public event Action<ISpell> OnHit;
        
        public Transform Transform { get; }
    }
}
