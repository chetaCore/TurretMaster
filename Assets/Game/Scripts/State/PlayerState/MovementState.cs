using Assets.Game.Scripts.Services;
using UnityEngine;

namespace Assets.Game.Scripts.State
{
    internal class MovementState : MonoBehaviour, IState
    {
        private CharacterMover _characterMover;
        private IInputService _inputService;

        public MovementState(CharacterMover characterMover)
        {
            _characterMover = characterMover;
        }

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            //if(_inputService.IsBuildButtonUp())
                
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