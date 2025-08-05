using System.Collections;
using Fantasy.SharedKernel.Weapons;
using UnityEngine;

namespace Fantasy.Commands
{
    internal sealed class CommandInvoker : MonoBehaviour
    {
        private AttackCommand _attackCommand;
        
        private void Awake()
        {
            if (TryGetComponent(out IWeaponHolder weaponHolder))
            {
                _attackCommand = new AttackCommand(weaponHolder);
            }
        }

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(3f, 5f));
                
                _attackCommand.Execute();
            }
        }
    }
}
