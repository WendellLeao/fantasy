namespace Fantasy.Utilities
{
    public static class PathUtility
    {
        public const string HealthMenuPath = GameplayMenuPath + "/Health";
        public const string WeaponsMenuPath = GameplayMenuPath + "/Weapons";
        public const string SpellsMenuPath = GameplayMenuPath + "/Spells";
        public const string DamageMenuPath = GameplayMenuPath + "/Damage";
        
        public const string CommandMenuPath = CreateAssetMenuPath + "/Commands";
        
        private const string GameplayMenuPath = CreateAssetMenuPath + "/Gameplay";
        
        private const string CreateAssetMenuPath = "Fantasy";
    }
}
