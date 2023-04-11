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
            _gameLoopService.GameLoopStateChangedEvent += GameLoopBehaviour;
            _turretInstaller.StartInstallEvent += GoToIdleState;

            return this;
        }

        private void OnDestroy()
        {
            _gameLoopService.GameLoopStateChangedEvent -= GameLoopBehaviour;
            _turretInstaller.StartInstallEvent += GoToIdleState;
        }

        private void GoToIdleState()
        {
            _characterStateMachine.Enter<CharacterInstallState>();
        }

        private void GameLoopBehaviour(GameLoopState gameLoopstate)
        {
            if (gameLoopstate == GameLoopState.VaitingStartGame || gameLoopstate == GameLoopState.StageEnded || gameLoopstate == GameLoopState.Victory)
            {
                _characterStateMachine.Enter<CharacterIdleState>();
            }
        }

        public void Enter()
        {
            _characterMover.CanMove = true;
        }

        public void Exit()
        {
            _characterMover.CanMove = false;
            //_characterMover.Controller.Move(Vector3.zero);
        }
    }
}