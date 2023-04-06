using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Entity.Enemy
{
    public abstract class EnemyAttacker : MonoBehaviour
    {
        protected bool _isAttack;

        public bool IsAttack { get => _isAttack;}

        public abstract void Attack();
    }
}