using Assets.Game.Scripts.Entity.BaseEntityScripts;
using Assets.Game.Scripts.Entity.Enemy;
using UnityEngine;

namespace Assets.Game.Scripts.State.EnemyState
{
    public class EnemyDeathState : MonoBehaviour, IState
    {
        private EnemyAimer _enemyAimer;
        private EnemyObserver _enemyObserver;
        private EnemyDeath _enemyDeath;

        public IState Initialize(
            EnemyAimer enemyAimer, EnemyObserver enemyObserver, EnemyDeath enemyDeath)
        {
            _enemyAimer = enemyAimer;
            _enemyObserver = enemyObserver;
            _enemyDeath = enemyDeath;

            return this;
        }

        public void Enter()
        {
            _enemyDeath.Dying();
            _enemyAimer.CanAim = false;
            _enemyObserver.CanDetect = false;
        }

        public void Exit()
        {
        }
    }
}