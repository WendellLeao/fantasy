using Fantasy.Utilities;
using UnityEngine;

namespace Fantasy.Commands
{
    [CreateAssetMenu(menuName = PathUtility.CommandMenuPath + "/CommandCollectionData", fileName = "CommandCollectionData")]
    public sealed class CommandCollectionData : ScriptableObject
    {
        [SerializeField]
        private CommandData[] commandData;

        public bool TryGetCommandDataByType(CommandType type, out CommandData result)
        {
            foreach (CommandData data in commandData)
            {
                if (data.Type == type)
                {
                    result = data;
                    return true;
                }
            }

            Debug.LogError($"Wasn't possible to retrieve a command data for the type '{type}'");
            
            result = null;
            return false;
        }
    }
}
