using Assets.Game.Scripts.Entity.BaseEntityScripts;
using UnityEngine;

namespace Assets.Game.Scripts.Entity.Weapons
{
    public class WeaponAttacker : MonoBehaviour
    {
        private Weapon _weapon;

        public bool CanAttack;

        public Weapon Weapon { get => _weapon; set => _weapon = value; }

        private void OnTriggerEnter(Collider other)
        {
            if (!CanAttack) return;
            if ((Weapon.TargetMask & (1 << other.gameObject.layer)) != 0)
            {
                if (other.TryGetComponent(out IDamageTaker itakeDamage))
                {
                    itakeDamage.TakeDamage(Weapon.Damage);
                }
            }
        }
    }
}