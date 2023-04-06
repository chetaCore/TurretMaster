using Assets.Game.Scripts.Entity.Enemy;
using UnityEngine;

namespace Assets.Game.Scripts.State.EnemyState
{
    public class EnemyMovingState : MonoBehaviour, IState
    {
        protected EnemyStateMachine _enemyStateMachine;
        protected EnemyObserver _enemyObserver;
        protected EnemyMover _enemyMover;
        protected EnemyAttackTrigger _enemyAttackTrigger;
        protected EnemyAnimator _animator;
        protected bool _isActive = false;

        public IState Initialize(
            EnemyStateMachine enemyStateMachine, EnemyObserver enemyObserver, EnemyMover enemyMover, EnemyAttackTrigger enemyAttackTrigger, EnemyAnimator animator)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyObserver = enemyObserver;
            _enemyMover = enemyMover;
            _enemyAttackTrigger = enemyAttackTrigger;
            _animator = animator;

            return this;
        }

        protected virtual void Update()
        {
            if (!_isActive) return;

            _animator.SetMoveSpeed(_enemyMover.MeshAgent.velocity.magnitude);

            if (_enemyObserver.DetectedColliders[0] != null)
                _enemyMover.Target = _enemyObserver.DetectedColliders[0].transform;
            else
                _enemyMover.Target = null;

            foreach (var trigerCollider in _enemyAttackTrigger.DetectedColliders)
            {
                if (trigerCollider != null)
                    if (trigerCollider.Equals(_enemyObserver.DetectedColliders[0]))
                    {
                        _enemyStateMachine.Enter<EnemyAttackState>();
                        break;
                    }
            }
        }

        public void Enter()
        {
            _isActive = true;
            _enemyMover.MeshAgent.enabled = true;
            _enemyMover.CanMove = true;
            _enemyAttackTrigger.CanDetect = true;
            _enemyObserver.CanDetect = true;
        }

        public void Exit()
        {
            _isActive = false;
            _enemyMover.CanMove = false;
            _enemyAttackTrigger.CanDetect = false;
            _enemyObserver.CanDetect = false;
            _enemyMover.MeshAgent.enabled = false;
            _animator.SetMoveSpeed(0);
        }
    }
}