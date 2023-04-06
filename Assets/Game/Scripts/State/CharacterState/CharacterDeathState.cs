using Assets.Game.Scripts.Services.GameLoopService;
using Assets.Game.Scripts.Services;
using UnityEngine;

namespace Assets.Game.Scripts.State.CharacterState
{
    public class CharacterDeathState : MonoBehaviour, IState
    {
        private CharacterStateMachine _characterStateMachine;
        private IGameLoopService _gameLoopService;

        public IState Initialize(CharacterStateMachine characterStateMachine)
        {

            _characterStateMachine = characterStateMachine;
            _gameLoopService = AllServices.Container.Single<IGameLoopService>();

            return this;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}