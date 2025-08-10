using System.Collections;
using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay
{
    internal sealed class CommandInvoker : EntityComponent
    {
        private AttackCommand _attackCommand;
        private Coroutine _executeAttackCommandRoutine;

        public void Initialize(IWeaponHolder weaponHolder)
        {
            _attackCommand = new AttackCommand(weaponHolder);
            
            base.Initialize();
        }

        protected override void OnBegin()
        {
            base.OnBegin();
            
            _executeAttackCommandRoutine = StartCoroutine(ExecuteAttackCommandRoutine());
        }

        protected override void OnStop()
        {
            base.OnStop();
            
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
