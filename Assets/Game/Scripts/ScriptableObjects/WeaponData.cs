using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private WeaponType _type;

    public WeaponType Type
    { get { return _type; } }

    [SerializeField] private WeaponRangeType _rangeType;

    public WeaponRangeType RangeType
    { get { return _rangeType; } }

    [SerializeField] private LayerMask _targetMask;

    public LayerMask TargetMask
    { get { return _targetMask; } }

    [SerializeField] private string _name;

    public string Name
    { get { return _name; } }

    [SerializeField] private float _damage;

    public float Damage
    { get { return _damage; } }

    [SerializeField] private GameObject _model;

    public GameObject Model
    { get { return _model; } }
}