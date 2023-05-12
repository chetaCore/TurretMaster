using Assets.Game.Scripts.Entity.BaseEntityScripts;
using UnityEngine;

public class ProjectileData : ScriptableObject
{
    [SerializeField] private UserType _userType;

    public UserType UserType
    { get { return _userType; } }

    [SerializeField] private ProjectileType _type;

    public ProjectileType Type
    { get { return _type; } }

    [SerializeField] private LayerMask _targetMask;

    public LayerMask TargetMask
    { get { return _targetMask; } }

    [SerializeField] private float _damage;

    public float Damage
    { get { return _damage; } }
}