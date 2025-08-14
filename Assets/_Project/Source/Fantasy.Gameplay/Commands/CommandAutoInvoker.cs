using System.Collections;
using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Commands
{
    internal sealed class CommandAutoInvoker : EntityComponent, ICommandInvoker
    {
        private AttackCommand _attackCommand;
        private Coroutine _executeAttackCommandRoutine;

        public void SetUp(IWeaponHolder weaponHolder)
        {
            _attackCommand = new AttackCommand(weaponHolder);
            
            base.SetUp();
        }

        protected override void OnSetUp()
        {
            base.OnSetUp();
            
            _executeAttackCommandRoutine = StartCoroutine(ExecuteAttackCommandRoutine());
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            if (_executeAttackCommandRoutine != null)
            {
                StopCoroutine(_executeAttackCommandRoutine);
            }
        }

        private IEnumerator ExecuteAttackCommandRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(3f, 5f));
                
                _attackCommand.Execute();
            }
        }
    }
}
