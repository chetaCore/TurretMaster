namespace Assets.Game.Scripts.State.EnemyState.RangedEnemyState
{
    public class EnemyRangedSpawningState : EnemySpawningState
    {
        protected override void Update()
        {
            if (!_isActive) return;

            else if (_groundDetector.EntityOnGround)
            {
                _entityDescent.CanDescent = false;
                _enemyMover.MeshAgent.enabled = true;
                _enemyStateMachine.Enter<EnemyRangedMovingState>();
            }
        }
    }
}