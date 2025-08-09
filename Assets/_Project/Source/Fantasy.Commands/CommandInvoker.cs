using System.Collections;
using Fantasy.Domain.Health;
using Fantasy.Domain.Weapons;
using UnityEngine;

namespace Fantasy.Commands
{
    internal sealed class CommandInvoker : MonoBehaviour
    {
        private AttackCommand _attackCommand;
        private IHealth _health;
        private Coroutine _executeAttackCommandRoutine;

        private void Awake()
        {
            if (TryGetComponent(out IWeaponHolder weaponHolder))
            {
                _attackCommand = new AttackCommand(weaponHolder);
            }

            TryGetComponent(out _health);
        }

        private void OnEnable()
        {
            _health.OnDepleted += HandleHealthDepleted;
        }

        private void OnDisable()
        {
            _health.OnDepleted -= HandleHealthDepleted;
        }

        private void HandleHealthDepleted()
        {
            if (_executeAttackCommandRoutine != null)
            {
                StopCoroutine(_executeAttackCommandRoutine);
            }
        }

        private void Start()
        {
            _executeAttackCommandRoutine = StartCoroutine(ExecuteAttackCommandRoutine());
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
