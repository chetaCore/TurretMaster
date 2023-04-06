using UnityEngine;

namespace Assets.Game.Scripts.Entity.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        protected static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Fall = Animator.StringToHash("Fall");
        private static readonly int Idle = Animator.StringToHash("Idle");

        protected const string AttackAnimationName = "EnemyAttack";

        public void SetMoveSpeed(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }

        public virtual float PlayAttack()
        {
            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;

            foreach (AnimationClip clip in clips)
            {
                if (clip.name == AttackAnimationName)
                {
                    _animator.SetTrigger(Attack);
                    return clips.Length / 2;
                }
            }
            return 0;
        }

        public void PlayIdle() =>
           _animator.SetTrigger(Idle);

        public void PlayFall() =>
           _animator.SetTrigger(Fall);

        public void PlayDeath() =>
            _animator.SetTrigger(Death);
    }
}