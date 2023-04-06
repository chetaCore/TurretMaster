using Assets.Game.Scripts.Entity.BaseEntityScripts;
using Assets.Game.Scripts.Entity.Enemy;
using UnityEngine;

namespace Assets.Game.Scripts.State.EnemyState
{
    public class EnemySpawningState : MonoBehaviour, IState
    {
        protected EnemyAnimator _animator;
        protected EnemyStateMachine _enemyStateMachine;
        protected EnemyMover _enemyMover;

        protected GroundedDetector _groundDetector;
        protected EntityDescent _entityDescent;

        protected bool _isActive = false;

        public IState Initialize(
            EnemyStateMachine enemyStateMachine, EnemyMover enemyMover, EnemyAnimator animator)
        {
            _animator = animator;
            _enemyStateMachine = enemyStateMachine;
            _enemyMover = enemyMover;

            return this;
        }

        protected virtual void Update()
        {
            if (!_isActive) return;

            if (_groundDetector.EntityOnGround)
            {
                _entityDescent.CanDescent = false;
                _enemyMover.MeshAgent.enabled = true;
                _enemyStateMachine.Enter<EnemyMovingState>();
            }
        }

        public void Enter()
        {
            _animator.PlayFall();

            _groundDetector = gameObject.GetComponentInChildren<GroundedDetector>();
            _entityDescent = gameObject.GetComponent<EntityDescent>();
            _groundDetector.Entity = gameObject;

            _enemyMover.MeshAgent.enabled = false;
            _isActive = true;
            _groundDetector.gameObject.SetActive(true);
            _groundDetector.CanDetected = true;

            _entityDescent.CanDescent = true;
        }

        public void Exit()
        {
            _animator.PlayIdle();

            _isActive = false;

            _groundDetector.CanDetected = false;
        }
    }
}