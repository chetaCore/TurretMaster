using Assets.Game.Scripts.Entity.BaseEntityScripts;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Entity.Enemy
{
    public class Enemy : BaseEntity
    {
        [SerializeField] private EnemyDeath _enemyDeath;

        public override event Action TakeDamageEvent;
        public override event Action DeathEvent;

        private LayerMask _targetMask;

        public LayerMask TargetMask { get => _targetMask; set => _targetMask = value; }

        public override void TakeDamage(float damage)
        {
            CurrentHP -= damage;
            TakeDamageEvent?.Invoke();

            if (CurrentHP <= 0)
            {
                _gameObjectKeeperService.DecreaseCountLivingEnemy();
                DeathEvent.Invoke();
                _enemyDeath.Dying();
            }
        }

    }
}