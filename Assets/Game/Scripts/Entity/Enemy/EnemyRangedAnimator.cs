using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Entity.Enemy
{
    public class EnemyRangedAnimator : EnemyAnimator
    {
        protected new string AttackAnimationName = "EnemyShoot";

        public override float PlayAttack()
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
    }
}