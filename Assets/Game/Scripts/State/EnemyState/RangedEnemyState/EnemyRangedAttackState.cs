using Assets.Game.Scripts.Entity.Enemy;
using DG.Tweening;

namespace Assets.Game.Scripts.State.EnemyState.RangedEnemyState
{
    public class EnemyRangedAttackState : EnemyAttackState, IState
    {

        protected new RangedEnemyAttacker _enemyAttacker;

        public override IState Initialize(
          EnemyStateMachine enemyStateMachine, EnemyAttacker enemyAttacker, EnemyAimer enemyAimer, EnemyObserver enemyObserver, EnemyAnimator animator)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyAttacker = (RangedEnemyAttacker)enemyAttacker;
            _enemyAimer = enemyAimer;
            _enemyObserver = enemyObserver;
            _animator = animator;

            DOTween.Sequence();

            return this;
        }

        public override void Enter()
        {
            _vaitSeq = DOTween.Sequence();

            _enemyAimer.CanAim = true;
            _animator.PlayAttack();
            _enemyAttacker.Attack();

            _enemyAimer.Target = _enemyObserver.DetectedColliders[0].transform;

            _vaitSeq.AppendInterval(_enemyAttacker.Weapon.RateOfFire).
                OnComplete(() => _enemyStateMachine.Enter<EnemyRangedMovingState>());
        }

        public override void Exit()
        {
            _vaitSeq.Kill();
            // _enemyAimer.CanAim = false;
        }
    }
}