using Assets.Game.Scripts.Entity.Turret;
using UnityEngine;

namespace Assets.Game.Scripts.State.TurretState
{
    public class TurretObservingState : MonoBehaviour, IState
    {
        private TurretStateMachine _turretStateMachine;
        private TurretObserver _turretObserver;

        private bool _isActive = false;

        public IState Initialize (TurretStateMachine turretStateMachine, TurretObserver turretObserver)
        {
            _turretStateMachine = turretStateMachine;
            _turretObserver = turretObserver;

            return this;
        }

        private void Update()
        {
            if (!_isActive) return;
            if (_turretObserver.DetectedColliders[0] != null)
                _turretStateMachine.Enter<TurretShootingState>();
        }

        public void Enter()
        {
            _isActive = true;
            _turretObserver.CanDetect = true;
        }

        public void Exit()
        {
            _isActive = false;
        }
    }
}