using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Entity.Enemy
{
    public class RangedEnemyAttacker : EnemyAttacker
    {
        private RangedWeapon _weapon;

        public RangedWeapon Weapon { get => _weapon; set => _weapon = value; }

        public override void Attack()
        {
            _weapon.Shoot();
        }

   
    }
}