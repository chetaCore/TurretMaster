using Assets.Game.Scripts.Entity.Character;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using UnityEngine;

namespace Assets.Game.Scripts.State.CharacterState
{
    public class CharacterDanceState : MonoBehaviour, IState
    {
        private CharacterAnimator _animator;
        private CharacterStateMachine _characterStateMachine;
        private IGameLoopService _gameLoopService;

        public IState Initialize(CharacterStateMachine characterStateMachine, CharacterAnimator animator)
        {
            _animator = animator;
            _characterStateMachine = characterStateMachine;
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();
            _gameLoopService.GameLoopStateChangedEvent += (_) =>
            {
                if (_ == GameLoopState.GameStarted)
                {
                    transform.position = GameObject.FindGameObjectWithTag(Constans.LevelPlayerPoint).transform.position;
                    _characterStateMachine.Enter<CharacterMovingState>();
                }
            };

            return this;
        }

        private void OnDestroy()
        {
            _gameLoopService.GameLoopStateChangedEvent -= (_) =>
            {
                if (_ == GameLoopState.GameStarted)
                {
                    transform.position = GameObject.FindGameObjectWithTag(Constans.LevelPlayerPoint).transform.position;
                    _characterStateMachine.Enter<CharacterMovingState>();
                }
            };
        }

        public void Enter()
        {
            _animator.PlayDance();
        }

        public void Exit()
        {
            _animator.PlayIdle();
        }
    }
}