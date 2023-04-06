using Assets.Game.Scripts.Entity.Character;
using Assets.Game.Scripts.Entity.Turret;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using DG.Tweening;
using UnityEngine;

namespace Assets.Game.Scripts.State.CharacterState
{
    internal class CharacterInstallState : MonoBehaviour, IState
    {
        private CharacterStateMachine _characterStateMachine;
        private TurrentInstaller _turretInstaller;
        private CharacterAnimator _animator;
        private IGameLoopService _gameLoopService;

        public IState Initialize(CharacterStateMachine characterStateMachine, TurrentInstaller turrentInstaller, CharacterAnimator animator)
        {
            _characterStateMachine = characterStateMachine;
            _turretInstaller = turrentInstaller;
            _animator = animator;

            _gameLoopService = AllServices.Container.Single<IGameLoopService>();
            _gameLoopService.GameLoopStateChangedEvent += (_) =>
            {
                if (_ == GameLoopState.VaitingStartGame || _ == GameLoopState.StageEnded || _ == GameLoopState.Victory)
                {
                    _characterStateMachine.Enter<CharacterIdleState>();
                }
            };

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
            };
        }

        public void Enter()
        {
            _animator.PlayInstall();
            DOTween.Sequence().
                AppendInterval(_turretInstaller.InstallTime).
                OnComplete(() =>
                {
                    _characterStateMachine.Enter<CharacterMovingState>();
                    _animator.PlayIdle();
                });
        }

        public void Exit()
        {
        }
    }
}