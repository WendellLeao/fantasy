using Fantasy.Utilities;
using UnityEngine;

namespace Fantasy.Commands
{
    [CreateAssetMenu(menuName = PathUtility.CommandMenuPath + "/CommandData", fileName = "NewCommandData")]
    internal sealed class CommandData : ScriptableObject
    {
        [SerializeField]
        private KeyCode keyCode;
        [SerializeField]
        private CommandType type;

        public KeyCode KeyCode => keyCode;
        public CommandType Type => type;
    }
}
