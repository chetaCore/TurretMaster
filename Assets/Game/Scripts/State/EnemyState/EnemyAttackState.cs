using Assets.Game.Scripts.Entity.Enemy;
using DG.Tweening;
using UnityEngine;

namespace Assets.Game.Scripts.State.EnemyState
{
    public class EnemyAttackState : MonoBehaviour, IState
    {
        protected EnemyStateMachine _enemyStateMachine;
        protected EnemyAttacker _enemyAttacker;
        protected EnemyAimer _enemyAimer;
        protected EnemyObserver _enemyObserver;
        protected EnemyAnimator _animator;

        protected Sequence _vaitSeq;

        public virtual IState Initialize(
            EnemyStateMachine enemyStateMachine, EnemyAttacker enemyAttacker,
            EnemyAimer enemyAimer, EnemyObserver enemyObserver, EnemyAnimator animator)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyAttacker = enemyAttacker;
            _enemyAimer = enemyAimer;
            _enemyObserver = enemyObserver;
            _animator = animator;

            DOTween.Sequence();

            return this;
        }

        public virtual void Enter()
        {
            _vaitSeq = DOTween.Sequence();

            _enemyAttacker.Attack();

            _enemyAimer.Target = _enemyObserver.DetectedColliders[0].transform;
            _enemyAimer.CanAim = true;

            _vaitSeq.
               AppendInterval(_animator.PlayAttack()).
               OnComplete(() => _enemyStateMachine.Enter<EnemyMovingState>());
        }

        public virtual void Exit()
        {
            _vaitSeq.Kill();
            _enemyAimer.CanAim = false;
        }
    }
}