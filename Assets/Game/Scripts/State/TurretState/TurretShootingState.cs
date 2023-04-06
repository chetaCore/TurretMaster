using Assets.Game.Scripts.Entity.Character;
using Assets.Game.Scripts.Entity.Turret;
using UnityEngine;

namespace Assets.Game.Scripts.State.TurretState
{
    public class TurretShootingState : MonoBehaviour, IState
    {
        private TurretStateMachine _turretStateMachine;
        private TurretAimer _turretAimer;
        private TurretShooter _turretshooter;
        private TurretObserver _turretObserver;
        private bool _isActive;

        public IState Initialize(TurretStateMachine turretStateMachine, TurretAimer turretAimer, TurretObserver turretObserver, TurretShooter turretShooter)
        {
            _turretStateMachine = turretStateMachine;
            _turretAimer = turretAimer;
            _turretObserver = turretObserver;
            _turretshooter = turretShooter;
            return this;
        }

        private void Update()
        {
            if (!_isActive) return;

            if (_turretObserver.DetectedColliders[0] == null)
                _turretStateMachine.Enter<TurretObservingState>();
            else
                _turretAimer.Target = _turretObserver.DetectedColliders[0].transform;
        }

        public void Enter()
        {
            _isActive = true;
            _turretshooter.CanShoot = true;
            _turretAimer.CanAim = true;
        }

        public void Exit()
        {
            _turretshooter.CanShoot = false;
            _turretAimer.CanAim = false;
            _isActive = false;

        }
    }
}