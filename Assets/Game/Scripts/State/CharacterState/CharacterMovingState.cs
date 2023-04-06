using Assets.Game.Scripts.Entity.Turret;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using UnityEngine;

namespace Assets.Game.Scripts.State.CharacterState
{
    public class CharacterMovingState : MonoBehaviour, IState
    {
        private CharacterStateMachine _characterStateMachine;
        private CharacterMover _characterMover;
        private TurrentInstaller _turretInstaller;
        private IGameLoopService _gameLoopService;

        public IState Initialize(CharacterStateMachine characterStateMachine, CharacterMover characterMover, TurrentInstaller turrentInstaller)
        {
            _characterStateMachine = characterStateMachine;
            _characterMover = characterMover;
            _turretInstaller = turrentInstaller;

            _gameLoopService = AllServices.Container.Single<IGameLoopService>();
            _gameLoopService.GameLoopStateChangedEvent += (_) =>
            {
                if (_ == GameLoopState.VaitingStartGame || _ == GameLoopState.StageEnded || _ == GameLoopState.Victory)
                {
                    _characterStateMachine.Enter<CharacterIdleState>();
                }
            };

            _turretInstaller.StartInstallEvent += () => _characterStateMachine.Enter<CharacterInstallState>();

            return this;
        }

        private void OnDestroy()
        {
            _gameLoopService.GameLoopStateChangedEvent -= (_) =>
            {
                if (_ == GameLoopState.VaitingStartGame || _ == GameLoopState.StageEnded || _ == GameLoopState.Victory)
                {
                    _characterStateMachine.Enter<CharacterIdleState>();
                }
                _turretInstaller.StartInstallEvent -= () => _characterStateMachine.Enter<CharacterInstallState>();
            };
        }

        public void Enter()
        {
            _characterMover.CanMove = true;
        }

        public void Exit()
        {
            _characterMover.CanMove = false;
        }
    }
}