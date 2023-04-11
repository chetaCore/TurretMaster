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
        private static readonly int Dance = Animator.StringToHash("Dance");
        private static readonly int Death = Animator.StringToHash("Death");

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

        public void PlayDance()
        {
            _animator.SetTrigger(Dance);
        }

        public void PlayDeath()
        {
            _animator.SetTrigger(Death);
        }

        private void Update()
        {
            SetMoveSpeed(CharacterMover.Controller.velocity.magnitude);
        }
    }
}