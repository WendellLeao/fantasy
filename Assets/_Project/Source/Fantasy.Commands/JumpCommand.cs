using UnityEngine;

namespace Fantasy.Commands
{
    internal sealed class JumpCommand : ICommand
    {
        public void Execute()
        {
            Debug.Log("<color=white>[JumpCommand] Execute</color>");
        }
    }
}
