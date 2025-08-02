using UnityEngine;

namespace Fantasy.Gameplay
{
    internal interface ISpellFactory
    {
        public ISpell CastSpell(SpellData data, Vector3 position, Vector3 direction);
    }
}
