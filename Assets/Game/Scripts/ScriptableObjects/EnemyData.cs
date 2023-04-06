using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Entity/Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private EnemyType _type;

    public EnemyType Type
    { get { return _type; } }

    [SerializeField] private LayerMask _targetMask;

    public LayerMask TargetMask
    { get { return _targetMask; } }

    [SerializeField] private float _speed;

    public float Speed
    { get { return _speed; } }

    [SerializeField] private float _hp;

    public float HP
    { get { return _hp; } }

    [SerializeField] private float _fieldOfView;

    public float FieldOfView
    { get { return _fieldOfView; } }

    [SerializeField] private float _detectionInterval;

    public float DetectionInterval
    { get { return _detectionInterval; } }

    [SerializeField] private float _timeToTurn;

    public float TimeToTurn
    { get { return _timeToTurn; } }

    [SerializeField] private float _attackRange;

    public float AttackRange
    { get { return _attackRange; } }

    [SerializeField] private WeaponType _weaponType;

    public WeaponType WeaponType
    { get { return _weaponType; } }

    [SerializeField] private GameObject _model;

    public GameObject Model
    { get { return _model; } }

    [SerializeField] private GameObject _rope;

    public GameObject RopeModel
    { get { return _rope; } }
}