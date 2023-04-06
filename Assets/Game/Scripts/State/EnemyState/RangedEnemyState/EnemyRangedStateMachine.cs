using Assets.Game.Scripts.Entity.BaseEntityScripts;
using Assets.Game.Scripts.Entity.Enemy;
using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.State.EnemyState.RangedEnemyState
{
    public class EnemyRangedStateMachine : EnemyStateMachine
    {
        public override void Initialize(EnemyObserver enemyObserver, EnemyMover enemyMover, EnemyAttackTrigger enemyAttackTrigger, EnemyAttacker enemyAttacker, EnemyAimer enemyAimer, EnemyAnimator animator, EnemyDeath enemyDeath, Enemy enemy)
        {
            _enemy = enemy;

            _states = new Dictionary<Type, IState>()
            {
                [typeof(EnemyRangedSpawningState)] =
                gameObject.AddComponent<EnemyRangedSpawningState>().Initialize(this, enemyMover, animator),

                [typeof(EnemyRangedMovingState)] =
                 gameObject.AddComponent<EnemyRangedMovingState>().Initialize(this, enemyObserver, enemyMover, enemyAttackTrigger, animator),

                [typeof(EnemyRangedAttackState)] =
                gameObject.AddComponent<EnemyRangedAttackState>().Initialize(this, enemyAttacker, enemyAimer, enemyObserver, animator),

                [typeof(EnemyDeathState)] =
                gameObject.AddComponent<EnemyDeathState>().Initialize(enemyAimer, enemyObserver, enemyDeath),
            };

            _enemy.DeathEvent += () => Enter<EnemyDeathState>();
        }

        protected override void Start()
        {
            Enter<EnemyRangedSpawningState>();
        }
    }
}