using Assets.Game.Scripts.Entity.Enemy;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.State.EnemyState.RangedEnemyState
{
    public class EnemyRangedMovingState : EnemyMovingState
    {
        protected override void Update()
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
                        _enemyStateMachine.Enter<EnemyRangedAttackState>();
                        break;
                    }
            }
        }
    }
}