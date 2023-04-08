using UnityEngine;

namespace Assets.Game.Scripts.Entity.Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private CharacterMover _characterMover;

        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Install = Animator.StringToHash("TurretInstall");
        private static readonly int Idle = Animator.StringToHash("Idle");

        public CharacterMover CharacterMover { get => _characterMover; set => _characterMover = value; }

        public void SetMoveSpeed(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }

        public void PlayInstall()
        {
            _animator.SetTrigger(Install);
        }

        public void PlayIdle()
        {
            _animator.SetTrigger(Idle);
        }

        private void Update()
        {
            SetMoveSpeed(CharacterMover.Controller.velocity.magnitude);
        }
    }
}