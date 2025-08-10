using System.Collections.Generic;
using Fantasy.Commands;
using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay
{
    internal sealed class CommandInputReader : EntityComponent
    {
        [SerializeField]
        private CommandCollectionData commandCollectionData;
        
        private readonly Dictionary<CommandType, ICommand> _commands = new();

        public void Initialize(IWeaponHolder weaponHolder)
        {
            _commands.Add(CommandType.Attack, new AttackCommand(weaponHolder));
            
            base.Initialize();
        }
        
        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);

            ReadCommands();
        }

        private void ReadCommands()
        {
            foreach (KeyValuePair<CommandType, ICommand> keyValuePair in _commands)
            {
                if (HasPressedCommandKey(keyValuePair.Key))
                {
                    ICommand command = keyValuePair.Value;
                    
                    command.Execute();
                }
            }
        }

        private bool HasPressedCommandKey(CommandType commandType)
        {
            if (!commandCollectionData.TryGetCommandDataByType(commandType, out CommandData data))
            {
                return false;
            }
            
            return Input.GetKeyDown(data.KeyCode);
        }
    }
}
