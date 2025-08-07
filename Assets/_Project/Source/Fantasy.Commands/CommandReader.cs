using System.Collections.Generic;
using Fantasy.Domain.Health;
using Fantasy.Domain.Weapons;
using UnityEngine;

namespace Fantasy.Commands
{
    internal sealed class CommandReader : MonoBehaviour
    {
        [SerializeField]
        private CommandCollectionData commandCollectionData;
        
        private readonly Dictionary<CommandType, ICommand> _commands = new();
        private IDamageable _damageable;
        private bool _canExecuteCommand = true;

        private void Awake()
        {
            if (TryGetComponent(out IWeaponHolder weaponHolder))
            {
                _commands.Add(CommandType.Attack, new AttackCommand(weaponHolder));
            }
            
            TryGetComponent(out _damageable);
        }
        
        private void OnEnable()
        {
            _damageable.OnDied += HandleDamageableDied;
        }

        private void OnDisable()
        {
            _damageable.OnDied -= HandleDamageableDied;
        }
        
        private void Update()
        {
            if (!_canExecuteCommand)
            {
                return;
            }
            
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
        
        private void HandleDamageableDied()
        {
            _canExecuteCommand = false;
        }
    }
}
