using Assets.Game.Scripts.Entity.Character;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using DG.Tweening;
using UnityEngine;

namespace Assets.Game.Scripts.State.CharacterState
{
    public class CharacterDeathState : MonoBehaviour, IState
    {
        private CharacterAnimator _animator;
        private CharacterStateMachine _characterStateMachine;
        private IGameLoopService _gameLoopService;

        public IState Initialize(CharacterStateMachine characterStateMachine, CharacterAnimator animator)
        {
            _animator = animator;
            _characterStateMachine = characterStateMachine;
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();

            return this;
        }

        public void Enter()
        {
            _animator.PlayDeath();
            DOTween.Sequence().
                AppendInterval(3f).
                OnComplete(() => _gameLoopService.ChangeGameLoopState(GameLoopState.Defeat));
        }

        public void Exit()
        {
        }
    }
}