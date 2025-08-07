using System.Collections;
using Fantasy.Domain.Health;
using Fantasy.Domain.Weapons;
using UnityEngine;

namespace Fantasy.Commands
{
    internal sealed class CommandInvoker : MonoBehaviour
    {
        private AttackCommand _attackCommand;
        private IDamageable _damageable;
        private Coroutine _executeAttackCommandRoutine;

        private void Awake()
        {
            if (TryGetComponent(out IWeaponHolder weaponHolder))
            {
                _attackCommand = new AttackCommand(weaponHolder);
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

        private void HandleDamageableDied()
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
