using Fantasy.Utilities;
using UnityEngine;

namespace Fantasy.Commands
{
    [CreateAssetMenu(menuName = PathUtility.CommandMenuPath + "/CommandCollectionData", fileName = "CommandCollectionData")]
    internal sealed class CommandCollectionData : ScriptableObject
    {
        [SerializeField]
        private CommandData[] commandData;

        public bool TryGetCommandDataByType(CommandType type, out CommandData data)
        {
            foreach (CommandData d in commandData)
            {
                if (d.Type == type)
                {
                    data = d;
                    return true;
                }
            }

            Debug.LogError($"Wasn't possible to retrieve a command data for the type '{type}'");
            
            data = null;
            return false;
        }
    }
}
