using Assets.Game.Scripts.Entity.BaseEntityScripts;
using Assets.Game.Scripts.Entity.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.State.EnemyState
{
    public class EnemyStateMachine : MonoBehaviour
    {
        protected Enemy _enemy;
        protected Dictionary<Type, IState> _states;
        protected IState _activeState;

        public virtual void Initialize(EnemyObserver enemyObserver, EnemyMover enemyMover, EnemyAttackTrigger enemyAttackTrigger, EnemyAttacker enemyAttacker, EnemyAimer enemyAimer, EnemyAnimator animator, EnemyDeath enemyDeath, Enemy enemy)
        {
            _enemy = enemy;

            _states = new Dictionary<Type, IState>()
            {
                [typeof(EnemySpawningState)] =
                gameObject.AddComponent<EnemySpawningState>().Initialize(this, enemyMover, animator),

                [typeof(EnemyMovingState)] =
                 gameObject.AddComponent<EnemyMovingState>().Initialize(this, enemyObserver, enemyMover, enemyAttackTrigger, animator),

                [typeof(EnemyAttackState)] =
                gameObject.AddComponent<EnemyAttackState>().Initialize(this, enemyAttacker, enemyAimer, enemyObserver, animator),

                [typeof(EnemyDeathState)] =
                gameObject.AddComponent<EnemyDeathState>().Initialize(enemyAimer, enemyObserver, enemyDeath),
            };

            _enemy.DeathEvent += () => Enter<EnemyDeathState>();
        }

        protected virtual void Start()
        {
            Enter<EnemySpawningState>();
        }

        private void OnDestroy()
        {
            _enemy.DeathEvent -= () => Enter<EnemyDeathState>();
        }

        public void Enter<TState>() where TState : IState
        {
            _activeState?.Exit();
            IState state = _states[typeof(TState)];
            _activeState = state;
            state.Enter();
        }
    }
}