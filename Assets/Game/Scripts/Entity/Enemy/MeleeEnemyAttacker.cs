using Assets.Game.Scripts.Entity.Weapons;
using DG.Tweening;

namespace Assets.Game.Scripts.Entity.Enemy
{
    public class MeleeEnemyAttacker : EnemyAttacker
    {
        private WeaponAttacker _weaponAttacker;

        private void Start()
        {
            _weaponAttacker = GetComponentInChildren<WeaponAttacker>();
        }

        public override void Attack()
        {
            _isAttack = true;
            _weaponAttacker.CanAttack = true;

            DOTween.Sequence().AppendInterval(2f).OnComplete(() =>
            {
                _weaponAttacker.CanAttack = false;
                _isAttack = false;
            });
        }
    }
}