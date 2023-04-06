using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeapon", menuName = "Weapon/RangedWeapon")]
public class RangedWeaponData : WeaponData
{
    [SerializeField] private float _rateOfFire;

    public float RateOfFire
    { get { return _rateOfFire; } }

    [SerializeField] private ProjectileType _bulletType;

    public ProjectileType BulletType
    { get { return _bulletType; } }
}