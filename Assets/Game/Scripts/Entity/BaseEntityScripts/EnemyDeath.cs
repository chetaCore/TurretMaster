using Assets.Game.Scripts.Entity.Enemy;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Entity.BaseEntityScripts
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        private EnemyAnimator _animator;

        public EnemyAnimator Animator { get => _animator; set => _animator = value; }

        public void Dying()
        {
            _collider.enabled = false;
            Animator.PlayDeath();
            Destroy(gameObject, 3f);
        } 
       
    }
}