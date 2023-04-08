using Assets.Game.Scripts.Entity.Character;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.GameLoopService;
using UnityEngine;

namespace Assets.Game.Scripts.State.CharacterState
{
    public class CharacterIdleState : MonoBehaviour, IState
    {
        private CharacterStateMachine _characterStateMachine;
        private IGameLoopService _gameLoopService;

        public IState Initialize(CharacterStateMachine characterStateMachine)
        {
            _characterStateMachine = characterStateMachine;
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();
            _gameLoopService.GameLoopStateChangedEvent += (_) =>
            {
                if (_ == GameLoopState.GameStarted || _ == GameLoopState.StageStarted || _ == GameLoopState.VaitingNextStage)
                {
                    _characterStateMachine.Enter<CharacterMovingState>();
                }
            };
            
            return this;
        }

        private void OnDestroy()
        {
            _gameLoopService.GameLoopStateChangedEvent -= (_) =>
            {
                if (_ == GameLoopState.VaitingStartGame || _ == GameLoopState.StageStarted || _ == GameLoopState.VaitingNextStage)
                {
                    _characterStateMachine.Enter<CharacterMovingState>();
                }
            };
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}