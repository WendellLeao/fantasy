using System.Collections.Generic;
using Fantasy.SharedKernel.Weapons;
using UnityEngine;

namespace Fantasy.Commands
{
    internal sealed class CommandReader : MonoBehaviour
    {
        [SerializeField]
        private CommandCollectionData commandCollectionData;
        
        private readonly Dictionary<CommandType, ICommand> _commands = new();

        private void Awake()
        {
            if (TryGetComponent(out IWeaponHolder weaponHolder))
            {
                _commands.Add(CommandType.Attack, new AttackCommand(weaponHolder));
            }
        }

        private void Update()
        {
            foreach (KeyValuePair<CommandType, ICommand> keyValuePair in _commands)
            {
                CommandType commandType = keyValuePair.Key;

                if (!commandCollectionData.TryGetCommandDataByType(commandType, out CommandData data))
                {
                    continue;
                }
                
                if (Input.GetKeyDown(data.KeyCode))
                {
                    ICommand command = keyValuePair.Value;
                    
                    command.Execute();
                }
            }
        }
    }
}
