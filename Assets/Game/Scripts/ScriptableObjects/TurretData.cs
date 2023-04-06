using Assets.Game.Scripts;
using Assets.Game.Scripts.Entity.Turret;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Entity/Turret")]
public class TurretData : ScriptableObject
{
    [SerializeField] private int _id;

    public int Id
    { get { return _id; } }

    [Range(0f, Constans.MaxValueOpeningProgress)]
    [SerializeField] private float _openingProgress;

    public float OpeningProgress
    { get { return _openingProgress; } set { _openingProgress = value; } }

    [SerializeField] private TurretType _type;

    public TurretType Type
    { get { return _type; } }

    [SerializeField] private LayerMask _targetMask;

    public LayerMask TargetMask
    { get { return _targetMask; } }

    [SerializeField] private string _name;

    public string Name
    { get { return _name; } }

    [SerializeField] private float _hp;

    public float Hp
    { get { return _hp; } }

    [SerializeField] private float _fieldOfView;

    public float FieldOfView
    { get { return _fieldOfView; } }

    [SerializeField] private float _detectionInterval;

    public float DetectionInterval
    { get { return _detectionInterval; } }

    [SerializeField] private float _rateOfFire;

    public float RateOfFire
    { get { return _rateOfFire; } }

    [SerializeField] private float _timeToTurn;

    public float TimeToTurn
    { get { return _timeToTurn; } }

    [SerializeField] private ProjectileType _projectileType;

    public ProjectileType ProjectileType
    { get { return _projectileType; } }

    [SerializeField] private float _rechargeTime;

    public float RechargeTime
    { get { return _rechargeTime; } }

    [SerializeField] private GameObject _model;

    public GameObject Model
    { get { return _model; } }

    [SerializeField] private Sprite _sprite;

    public Sprite Sprite
    { get { return _sprite; } }
}