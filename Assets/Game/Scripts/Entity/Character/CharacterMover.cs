using Assets.Game.Scripts.Entity.Character;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.State;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private Player _player;
        private IInputService _inputService;
        private Camera _camera;

        public bool CanMove = false;

        public CharacterController Controller { get => _controller;}

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Start()
        {
            _camera = Camera.main;
            CameraFollow();
        }


        private void Update()
        {
            if (!CanMove) return;

            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constans.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.z = movementVector.y;
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            Controller.Move(movementVector * _player.Speed * Time.deltaTime);
        }

        private void CameraFollow() => 
            _camera.GetComponent<CameraFollow>().Follow(gameObject);
    }
}