namespace Fantasy.Gameplay
{
    internal interface ISpellCaster
    {
        public void CastSpell();
        public void SetSpellFactory(ISpellFactory spellFactory);
    }
}
